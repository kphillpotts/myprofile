using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Script.Services;
using System.Web.Services;
using System.Xml;

namespace BackOfficeService
{
    /// <summary>
    /// Summary description for IntegrationService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/BackOffice")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ScriptService]    
    public class BackOfficeService : WebService
    {
        private const string TW_CLASSIFICATION_CODE = "TWM";
        private const string STD_CLASSIFICATION_CODE = "STD";
        private const int TW_DEPT_CAT_SYNC_INTRVL = 5; // 5 minutes interval
        private const int TW_DEPT_CAT_CODE_MAX_LENGTH = 50;
        
        #region General Web Methods
        [WebMethod]
        public bool Ping()
        {
            return true;
        }

        /// <summary>
        /// Gets the fred office version.
        /// </summary>
        /// <returns></returns>
        [WebMethod]
        public string GetBackOfficeVersion()
        {
            try
            {
                //string version = Assembly.Load("FredOffice.Model").GetName().Version.ToString();                
                //return Assembly.Load("FredOffice.Model").GetName().Version.ToString();
                return "1.0.0.0";
            }
            catch (Exception ex)
            {                
                throw;
            }
        }
        #endregion

        #region Product Management Services
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        // ReSharper disable InconsistentNaming
        public List<string> GetHeadOfficeLinkedProducts(string userName, string password)
        // ReSharper restore InconsistentNaming
        {
            AuthenticateWebServiceUser(userName, password);
                      
            // ReSharper disable InconsistentNaming
            List<string> itemHeadOfficeIDs = new List<string>();
            // ReSharper restore InconsistentNaming

            try
            {
                //const string sqlQuery = @"    Select HeadOfficeID from doItem with (nolock) where HeadOfficeID > 0    ";
                //GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
                //DataTable resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, null);

                //foreach (string HeadOfficeId in resultTable.Rows.Cast<DataRow>()
                //            .Select(row => row["HeadOfficeID"].ToString())
                //            .Where(HeadOfficeId => !itemHeadOfficeIDs.Contains(HeadOfficeId)))
                //    itemHeadOfficeIDs.Add(HeadOfficeId);                
            }
            catch (Exception ex)
            {               
                throw;
            }
            FileLogger.InformationEntry("Web Service - Successfully completed the Request to Get List of Item HeadOffice IDs");
            return itemHeadOfficeIDs;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        // ReSharper disable InconsistentNaming
        public List<string> GetHeadOfficeLinkedSuppliers(string userName, string password)
        // ReSharper restore InconsistentNaming
        {
            FileLogger.InformationEntry("Web Service - Authenticating in GetHeadOfficeLinkedSuppliers");

            AuthenticateWebServiceUser(userName, password);

            FileLogger.InformationEntry("Web Service - Authentication Successful");
            // ReSharper disable InconsistentNaming
            List<string> supplierHeadOfficeIDs = new List<string>();
            // ReSharper restore InconsistentNaming
            try
            {
                //const string sqlQuery = @"  Select HeadOfficeID from doSupplier with (nolock) where HeadOfficeID > 0    ";
                //GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
                //DataTable resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, null);
                //FileLogger.VerboseEntry("Results Retrieved for HeadOffice linked Suppliers");

                //foreach (string HeadOfficeId in resultTable.Rows.Cast<DataRow>()
                //    .Select(row => row["HeadOfficeID"].ToString())
                //    .Where(HeadOfficeId => !supplierHeadOfficeIDs.Contains(HeadOfficeId)))
                //    supplierHeadOfficeIDs.Add(HeadOfficeId);
                FileLogger.VerboseEntry("Ready to return the List of String to Client");
            }
            catch (Exception ex)
            {
                FileLogger.ErrorEntry(
                                     "Error Occured getting the HeadOffice Linked Suppliers. Details - " + ex.Message,
                                     ex);
                throw;
            }
            FileLogger.InformationEntry(
                                 "Successfully completed the Request in GetHeadOfficeLinkedSuppliers");
            return supplierHeadOfficeIDs;
        }

        /// <summary>
        /// Creates or updates supplier items in Fred Office based on Item's HeadOfficeID and Supplier HQID.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="supplierProducts">The supplier products.</param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProcessResult> CreateUpdateSupplierProducts(string userName, string password, List<SupplierProduct> supplierProducts)
        {
            List<ProcessResult> results = new List<ProcessResult>();
            try
            {
                // Check user name password
                FileLogger.InformationEntry(string.Format("Authenticating in CreateUpdateSupplierItems"));
                AuthenticateWebServiceUser(userName, password);

                FileLogger.InformationEntry(
                                     string.Format("Received '{0}' of supplieritems to create or update",
                                                   supplierProducts.Count));

                foreach (SupplierProduct supplierProduct in supplierProducts)
                {
                    string processResultKey = "Item HQID : " + supplierProduct.ProductHQID + " Supplier HQID : " +
                                               supplierProduct.SupplierHQID;

                    if (supplierProduct.ProductHQID <= 0 || supplierProduct.SupplierHQID <= 0)
                    {
                        string errorMessage = string.Format("Invalid  Item HQID: {0} or Supplier HQID: {1}. Supplier item is not created/updated.", supplierProduct.ProductHQID, supplierProduct.SupplierHQID);
                        FileLogger.WarningEntry(errorMessage);
                        results.Add(CreateProcessResult(processResultKey, errorMessage));
                        continue;
                    }

                    if (IsNullOrWhiteSpace(supplierProduct.ReorderNumber))
                    {
                        string errorMessage = string.Format("Reorder number cannot be null or blank");
                        FileLogger.WarningEntry(errorMessage);
                        results.Add(CreateProcessResult(processResultKey, errorMessage));
                        continue;
                    }

                    // Remove the blank spaces before processing further.
                    supplierProduct.ReorderNumber = supplierProduct.ReorderNumber.Trim();

                    try
                    {
                        // check if the supplierListItem already exists in the database update it
                        #region Fixit
                        //                        string sqlQuery = string.Format(@" SELECT TOP 1 [doSupplierItem].[ID] 
                        //                                FROM [doSupplierItem] with (nolock)
                        //                                INNER JOIN [doSupplier] with (nolock) on [doSupplier].[ID] = [doSupplierItem].[Supplier]
                        //                                INNER JOIN [doItem]  with (nolock)on [doItem].[ID] = [doSupplierItem].[ItemSupplied]
                        //                                WHERE [doSupplier].[HeadOfficeID] = @SupplierHQID AND [doItem].[HeadOfficeID] = @ProductHQID ");

                        //                        CustomDbParameter[] parameters = new[]
                        //                                                 {
                        //                                                     new CustomDbParameter("@SupplierHQID", supplierProduct.SupplierHQID),
                        //                                                     new CustomDbParameter("@ProductHQID",supplierProduct.ProductHQID)
                        //                                                 };

                        //                        GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
                        //                        DataTable resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, parameters);
                        //                        if (resultTable.Rows.Count > 0)
                        //                        {
                        //                            FileLogger.VerboseEntry(
                        //                                string.Format(
                        //                                    "Supplier Item found with supplier's HeadOfficeID '{0}' and item's HeadOfficeID '{1}'",
                        //                                    supplierProduct.SupplierHQID, supplierProduct.ProductHQID));
                        //                            long supplierItemId = resultTable.Rows[0].Field<long>("ID");
                        //                            SupplierItem supplierItem =
                        //                                DataContext.Session[new Key(supplierItemId)] as SupplierItem;
                        //                            if (supplierItem != null)
                        //                            {
                        //                                FileLogger.VerboseEntry(
                        //                                    string.Format("Updating found supplier item with data object ID '{0}'",
                        //                                                  supplierItemId));

                        //                                // check if it is unique for this supplier only if it is different reorder number other than current one.
                        //                                if (supplierItem.ReorderNumber != supplierProduct.ReorderNumber)
                        //                                {
                        //                                    FileLogger.VerboseEntry(
                        //                                                             string.Format(
                        //                                                                 "Checking if Reordernumber '{0}'is linked to another item under this supplier.",
                        //                                                                 supplierProduct.ReorderNumber));
                        //                                    bool duplicateReOrderNumber =
                        //                                        HeadOfficeDataService.CheckDuplicateSupplierItem(DataContext.Session,
                        //                                                                                     new Key(0),  //no need to pass item key
                        //                                                                                     supplierItem.Supplier.Key,
                        //                                                                                     supplierProduct.ReorderNumber,
                        //                                                                                     true);
                        //                                    if (!duplicateReOrderNumber)
                        //                                    {
                        //                                        string errorMessage = string.Format(
                        //                                            "Cannot update SupplierItem with Reordernumber '{0}'. Because another item under this supplier has same Reordernumber.",
                        //                                            supplierProduct.ReorderNumber);
                        //                                        FileLogger.WarningEntry(errorMessage);
                        //                                        results.Add(CreateProcessResult(processResultKey, errorMessage));
                        //                                        continue;
                        //                                    }

                        //                                    supplierItem.ReorderNumber = supplierProduct.ReorderNumber;
                        //                                }

                        //                                supplierItem.PackQuantity = supplierProduct.PackSize;
                        //                                supplierItem.ItemSupplied.MSRP = supplierProduct.SupplierRRP;
                        //                                supplierItem.MinimumOrder = supplierProduct.MinOrderQty;
                        //                                supplierItem.Cost = supplierProduct.SupplierCost;
                        //                                // Update Preferred Supplier
                        //                                UpdatePreferredSupplier(supplierItem, supplierProduct);
                        //                                // Update Primary Supplier if it is already set.
                        //                                UpdatePrimarySupplier(supplierItem.Supplier, supplierItem.ItemSupplied, supplierProduct.SetPreferredAsPrimary);
                        //                            }
                        //                        }
                        //                        else
                        //                        {
                        //                            // create a new suppliler item
                        //                            FileLogger.VerboseEntry(string.Format("Supplier item is not found so creating a new one."));
                        //                            // check supplier if it exists
                        //                            sqlQuery = string.Format(
                        //                                @"  SELECT TOP 1 [doSupplier].[ID] 
                        //                                FROM [doSupplier] with (nolock)
                        //                                WHERE [doSupplier].[HeadOfficeID] = @HeadOfficeID");
                        //                            resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, new CustomDbParameter("@HeadOfficeID", supplierProduct.SupplierHQID));
                        //                            long supplierId = resultTable.Rows.Count > 0 ? resultTable.Rows[0].Field<long>("ID") : 0;

                        //                            Supplier supplier = supplierId == 0
                        //                                                    ? null
                        //                                                    : DataContext.Session[new Key(supplierId)] as Supplier;
                        //                            if (supplier == null)
                        //                            {
                        //                                string errorMessage = string.Format(
                        //                                    "Unable to create Supplier Item as supplier with HeadOfficeID : '{0}' is not found.",
                        //                                    supplierProduct.SupplierHQID);
                        //                                FileLogger.WarningEntry(errorMessage);
                        //                                results.Add(CreateProcessResult(processResultKey, errorMessage));
                        //                                continue;
                        //                            }

                        //                            FileLogger.VerboseEntry(
                        //                                string.Format(
                        //                                    "Found the supplier  with HeadOfficeID '{0}' for creating new supplier item ",
                        //                                    supplierProduct.SupplierHQID));
                        //                            // check item if it exists
                        //                            sqlQuery = string.Format(
                        //                                @"  SELECT TOP 1 [doItem].[ID] 
                        //                                FROM [doItem] with (nolock)
                        //                                WHERE [doItem].[HeadOfficeID] = @HeadOfficeID ");
                        //                            resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, new CustomDbParameter("@HeadOfficeID", supplierProduct.ProductHQID));
                        //                            long itemId = resultTable.Rows.Count > 0 ? resultTable.Rows[0].Field<long>("ID") : 0;
                        //                            Item item = itemId == 0 ? null : DataContext.Session[new Key(itemId)] as Item;
                        //                            if (item == null)
                        //                            {
                        //                                string errorMessage = string.Format(
                        //                                    "Unable to create Supplier Item as item with HeadOfficeID : '{0}' is not found.",
                        //                                    supplierProduct.ProductHQID);
                        //                                FileLogger.WarningEntry(errorMessage);
                        //                                results.Add(CreateProcessResult(processResultKey, errorMessage));

                        //                                continue;
                        //                            }

                        //                            FileLogger.VerboseEntry(
                        //                               string.Format("Found the item  with HeadOfficeID '{0}' for creating new supplier item ",
                        //                                             supplierProduct.ProductHQID));


                        //                            bool duplicateReOrderNumber =
                        //                                     HeadOfficeDataService.CheckDuplicateSupplierItem(DataContext.Session,
                        //                                                                                  new Key(0),  //no need to pass item key
                        //                                                                                  supplier.Key,
                        //                                                                                  supplierProduct.ReorderNumber,
                        //                                                                                  true);
                        //                            if (!duplicateReOrderNumber)
                        //                            {

                        //                                string errorMessage = string.Format(
                        //                                           "Cannot create SupplierItem with Reordernumber '{0}'. Because another item under this supplier has same Reordernumber.",
                        //                                           supplierProduct.ReorderNumber);
                        //                                FileLogger.WarningEntry(errorMessage);
                        //                                results.Add(CreateProcessResult(processResultKey, errorMessage));

                        //                            }
                        //                            else
                        //                            {
                        //                                // ready to create supplier item now
                        //                                SupplierItem supplierItem = SupplierItem.Create(DataContext.Session, new SupplierItem<DataObject>
                        //                                {
                        //                                    AggregateSource = SourceTypes.TerryWhite,
                        //                                    ItemSupplied = item,
                        //                                    Supplier = supplier,
                        //                                    PackQuantity =
                        //                                        supplierProduct.PackSize,
                        //                                    ReorderNumber =
                        //                                        supplierProduct.ReorderNumber,
                        //                                    MinimumOrder =
                        //                                        supplierProduct.MinOrderQty,
                        //                                    Cost = supplierProduct.SupplierCost
                        //                                });

                        //                                item.MSRP = supplierProduct.SupplierRRP;

                        //                                // Update Preferred Supplier
                        //                                UpdatePreferredSupplier(supplierItem, supplierProduct);

                        //                                UpdatePrimarySupplier(supplier, item, supplierProduct.SetPreferredAsPrimary);
                        //                                FileLogger.InformationEntry("Successfully created Supplier Item");
                        //                            }
                        //                        } 
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        FileLogger.ErrorEntry("Error Occured while creating/updating supplier items. Details - " + ex.Message, ex);
                        results.Add(
                            CreateProcessResult(processResultKey, "Exception Creating Supplier Item : " + ex.Message));
                    }
                }

            }
            catch (Exception ex)
            {
                results.Add(CreateProcessResult(ex.Message, ex.StackTrace));
                FileLogger.ErrorEntry("Error Occured while creating/updating supplier items. Details - " + ex.Message, ex);
            }

            return results;
        }

        /// <summary>
        /// Creates or updates items in Fred Office based on Item's HeadOfficeID
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="products">The products.</param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProcessResult> CreateUpdateProducts(string userName, string password, List<Product> products)
        {
            List<ProcessResult> processErrorList = new List<ProcessResult>();
            try
            {
                // Check user name password
                FileLogger.InformationEntry(string.Format("Authenticating in CreateUpdateProducts"));
                AuthenticateWebServiceUser(userName, password);

                FileLogger.InformationEntry(string.Format("Received '{0}' of items to create or update", products.Count));

                #region Fixit
                //// Get Classifications to use it inside the loop
                //ItemClassification twClassification = ItemClassification.GetByCode(DataContext.Session, TW_CLASSIFICATION_CODE);
                //ItemClassification stdClassification = ItemClassification.GetByCode(DataContext.Session, STD_CLASSIFICATION_CODE);
                //foreach (Product product in products)
                //{
                //    string processResultKey = "Item HQID : " + product.ProductHQID;
                //    try
                //    {
                //        // if invalid item HQID passed in continue to next item.
                //        if (product.ProductHQID <= 0)
                //        {
                //            string errorMessage = string.Format("Invalid  Item HQID: {0}. Item is not created/updated.", product.ProductHQID);
                //            CreateProcessError(processResultKey, processErrorList, errorMessage);
                //            continue;
                //        }
                //        GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
                //        Model.Department department = null;
                //        Model.Category category = null;
                //        Supplier manufacturer = null;


                //        // Check Department, Category, Manufacturer Code
                //        if (!IsNullOrWhiteSpace(product.DepartmentCode))
                //        {
                //            department = GetDepartmentByCode(DataContext.Session, generalDataService, product);
                //            if (department == null)
                //            {
                //                string errorMessage = string.Format("Invalid  Department Code: {0}. Item is not created/updated.", product.DepartmentCode);
                //                CreateProcessError(processResultKey, processErrorList, errorMessage);
                //                continue;
                //            }
                //        }

                //        if (!IsNullOrWhiteSpace(product.CategoryCode))
                //        {
                //            category = GetCategoryByCode(DataContext.Session, generalDataService, product);
                //            if (category == null)
                //            {
                //                string errorMessage = string.Format("Invalid  Category Code: {0}. Item is not created/updated.", product.CategoryCode);
                //                CreateProcessError(processResultKey, processErrorList, errorMessage);
                //                continue;
                //            }
                //        }

                //        if (product.ManufacturerHQID > 0)
                //        {
                //            manufacturer = GetSupplierByHQID(DataContext.Session, generalDataService, product);
                //            if (manufacturer == null)
                //            {
                //                string errorMessage = string.Format("Invalid  Manufacturer HQID: {0}. Item will be created/updated with no manufacturer.", product.ManufacturerHQID);
                //                CreateProcessError(processResultKey, processErrorList, errorMessage);
                //            }
                //        }

                //        product.DepartmentCode = product.DepartmentCode.Trim();
                //        product.CategoryCode = product.CategoryCode.Trim();
                //        product.Description = product.Description.Trim();

                //        long itemId = GetItemByHQID(product.ProductHQID, generalDataService);
                //        if (itemId > 0)
                //        {
                //            Item item = DataContext.Session[new Key(itemId)] as Item;
                //            if (item != null)
                //            {
                //                FileLogger.VerboseEntry(string.Format("Updating found item with data object ID '{0}'", itemId));
                //                FileLogger.VerboseEntry(string.Format("Checking if given aliases are duplicated with otheritems  while updating data object ID '{0}'", itemId));
                //                CodeGenerationService cgs = DataContext.Session.GetService<CodeGenerationService>();
                //                if (product.AppendBarcodes)
                //                {
                //                    // check if duplicate barcodes
                //                    if (HasDuplicatedBarcodes(cgs, item, product, processErrorList, false))
                //                        continue;
                //                }

                //                if (product.UpdateDescription)
                //                {
                //                    if (!IsNullOrWhiteSpace(product.Description))
                //                        item.Description = product.Description;
                //                    else
                //                    {
                //                        string errorMessage = string.Format("Empty Item Description for Item's HQID {0}. Item will not be updated.", product.ProductHQID);
                //                        CreateProcessError(processResultKey, processErrorList, errorMessage);
                //                        continue;
                //                    }
                //                }

                //                item.ItemType = (ItemType)((int)product.ProductType);
                //                if (product.UpdateTaxes)
                //                {
                //                    item.PurchaseTax = GetPurchaseTax(DataContext.Session, product);
                //                    item.ItemTax = GetItemTax(DataContext.Session, product);
                //                }
                //                if (product.UpdateItemLocation)
                //                    item.ItemLocation = product.ItemLocation;
                //                item.ItemClassification = product.TWManaged ? twClassification : stdClassification;
                //                item.Detail1 = product.Brand;
                //                item.Detail2 = product.Name;
                //                item.Detail3 = product.Size;
                //                item.InActive = product.Discontinued;
                //                item.TWManaged = product.TWManaged;

                //                // Add Aliases

                //                if (product.AppendBarcodes)
                //                {
                //                    AddBarcodes(cgs, item, product, processErrorList);
                //                    FileLogger.VerboseEntry(
                //                        string.Format("Successfully added aliases to item with HeadOfficeID '{0}'.",
                //                                      product.ProductHQID));
                //                }
                //                item.Department = department;
                //                item.Category = category;
                //                item.Manufacturer = manufacturer;

                //                FileLogger.VerboseEntry(string.Format("Successfully updated item with HeadOfficeID '{0}' details.", product.ProductHQID));
                //            }
                //            else
                //            {
                //                string errorMessage = string.Format(
                //                   "Item with Item HQID: {0} cannot be loaded. Item cannot be updated.", product.ProductHQID);
                //                FileLogger.InformationEntry(errorMessage);
                //                CreateProcessError(processResultKey, processErrorList, errorMessage);
                //            }
                //        }
                //        else
                //        {
                //            // create a new  item
                //            // Check Invalid barcode
                //            CodeGenerationService cgs = DataContext.Session.GetService<CodeGenerationService>();

                //            // check if given product has valid barcodes
                //            if (!HasValidBarcodes(product, processErrorList))
                //                continue;

                //            // check if duplicate barcodes
                //            if (HasDuplicatedBarcodes(cgs, null, product, processErrorList, true))
                //                continue;

                //            FileLogger.VerboseEntry(string.Format("Has Valid barcodes and not duplicated."));
                //            FileLogger.VerboseEntry(string.Format("Item is not found so creating a new one."));


                //            Item<DataObject> itemCommon = new Item<DataObject>();

                //            if (!IsNullOrWhiteSpace(product.Description))
                //                itemCommon.Description = product.Description;
                //            else
                //            {
                //                string errorMessage = string.Format("Empty Item Description for Item's HQID {0}. Item will not be created.", product.ProductHQID);
                //                CreateProcessError(processResultKey, processErrorList, errorMessage);
                //                continue;
                //            }
                //            itemCommon.AggregateSource = SourceTypes.TerryWhite;
                //            itemCommon.Code = cgs.GetNextValidCode(AutoCodeType.ItemCode);
                //            itemCommon.ItemClassification = product.TWManaged ? twClassification : stdClassification;
                //            itemCommon.HeadOfficeID = product.ProductHQID;
                //            itemCommon.ItemType = (ItemType)((int)product.ProductType);
                //            itemCommon.Department = department;
                //            itemCommon.Category = category;
                //            itemCommon.Manufacturer = manufacturer;
                //            itemCommon.ItemLocation = product.ItemLocation;
                //            itemCommon.Detail1 = product.Brand;
                //            itemCommon.Detail2 = product.Name;
                //            itemCommon.Detail3 = product.Size;
                //            itemCommon.TWManaged = product.TWManaged;
                //            itemCommon.InActive = product.Discontinued;  //no point creating discontinued item
                //            itemCommon.AutoCalcPriceLevel = true;
                //            itemCommon.PurchaseTax = GetPurchaseTax(DataContext.Session, product);
                //            itemCommon.ItemTax = GetItemTax(DataContext.Session, product);
                //            Item newItem = Item.Create(DataContext.Session, itemCommon);
                //            AddBarcodes(cgs, newItem, product, processErrorList, true);
                //            FileLogger.InformationEntry("Successfully created Item");
                //        }

               
                //    }
                //    catch (Exception ex)
                //    {
                //        FileLogger.ErrorEntry(
                //                             "Error Occured while creating/updating items. Details - " + ex.Message, ex);

                //        processErrorList.Add(CreateProcessResult(processResultKey,
                //                                                 "Exception Creating Item : " + ex.Message));
                //    }
                //}
         #endregion

            }
            catch (Exception ex)
            {
                processErrorList.Add(CreateProcessResult(ex.Message, ex.StackTrace));
                FileLogger.ErrorEntry(
                                     "Error Occured while creating/updating items. Details - " + ex.Message, ex);
            }

            FileLogger.InformationEntry(string.Format("Successfully ran CreateUpdateProducts for {0} products.", products.Count - processErrorList.Count));
            return processErrorList;
        }



        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProcessResult> CreateUpdateDepartments(string userName, string password, List<Department> departmentsList)
        {
            List<ProcessResult> results = new List<ProcessResult>();
            
            try
            {
                // Check user name password
                FileLogger.InformationEntry(
                                     string.Format("Authenticating in CreateUpdateDepartments"));
                AuthenticateWebServiceUser(userName, password);

                FileLogger.InformationEntry(string.Format("Authentication Successful. Received '{0}' of Departments to create or update", departmentsList.Count));

                #region FIXIT
                //DateTime lastSyncTime = gds.TWDeptCategoryLastSync;
                //FileLogger.InformationEntry(string.Format("Last TW Dept/Category sync time at the beginning: {0}", lastSyncTime));

                //if (lastSyncTime >= DateTime.Now)
                //{
                //    FileLogger.InformationEntry(string.Format("Last Sync time is not passed yet. LastTWDeptCategorySyncTime : {0}", lastSyncTime));
                //    results.Add(CreateProcessResult("CreateUpdateDeapartments", "Update to Departments and Categories is in progress now. Please try again in few minutes"));
                //    return results;
                //}
                //// Add 5 minutes interval so that incase of another client calling this method will be rejected in next 5 minutes
                //gds.TWDeptCategoryLastSync = DateTime.Now.AddMinutes(TW_DEPT_CAT_SYNC_INTRVL);

                //FileLogger.InformationEntry(string.Format("Last TW Dept/Category sync time before updating depts: {0}", gds.TWDeptCategoryLastSync));

                //// Truncate the department codes to max 50 before further process
                //foreach (Department department in departmentsList)
                //{
                //    department.Code = TruncateCode(department.Code);
                //}


                //// Dept creation is special. Its Either All In or All Out - even on one error.
                //// This loop is the first parse to identify the rogue records 
                //foreach (Department department in departmentsList)
                //{
                //    string processResultKey = department.Code;
                //    FileLogger.VerboseEntry(
                //                         "Processing... Create/ Update for Dept with Code - " + processResultKey);

                //    if (department.Code == String.Empty || department.Name == String.Empty)
                //    {
                //        string errorMessage =
                //            string.Format("Invalid Data Specified: Code - {0}, Name - {1}. Department can not be created/updated.",
                //                          department.Code, department.Name);
                //        FileLogger.WarningEntry(errorMessage);
                //        results.Add(CreateProcessResult(processResultKey, errorMessage));
                //    }
                //}

                //if (results.Count > 0)
                //{
                //    string errorMsg = string.Format(
                //        "Aborting Department Creation. Error Occured in {0} records in the pre-check.",
                //        results.Count);
                //    FileLogger.InformationEntry(errorMsg);
                //    results.Add(CreateProcessResult(string.Empty, errorMsg));
                //    return results;
                //}

                //// Check if the Dept is being created the first time. If first time the service is called, delete all the Fred Office Dept/Category
                //int twDeptCount = GetTWMDepartmentCount(DataContext.Session);
                //FileLogger.InformationEntry(string.Format("Is the Heirarchy enforced already? - {0}", (twDeptCount > 0) ? "true" : "false"));
                //bool firstRun = (twDeptCount == 0);

                //if (firstRun)
                //{
                //    gds.TWDeptCategoryLastSync = DateTime.Now.AddMinutes(TW_DEPT_CAT_SYNC_INTRVL);
                //    FileLogger.InformationEntry("Beginning to Remove Fred Office Departments since the service is running for the first time.");
                //    RemoveTerryWhiteDeptCategories(DataContext.Session, true);
                //    FileLogger.InformationEntry("Successfully completed removing Fred Office Departments & Categories.");
                //}


                //TransactionController deptTransactions =
                //    DataContext.Session.CreateTransactionController(TransactionMode.TransactionRequired |
                //                                                    TransactionMode.Unsafe);
                //string currentTWDeptCode = String.Empty;
                //int updateCounter = 0;
                //int insertCounter = 0;
                //try
                //{
                //    // Load the existing Departments - Key : Code - Value - Dept
                //    Dictionary<string, Model.Department> existingOnlineDepartmentDictionary = new Dictionary<string, Model.Department>();
                //    if (!firstRun)
                //    {
                //        FileLogger.InformationEntry("Not a First Run - Getting the Existing Departments Preloaded.");
                //        Query query =
                //            DataContext.Session.CreateQuery(
                //                "select Department instances where {IsOtherCostDepartment} = 0 and {IsDefault} = 0 ");
                //        QueryResult qrOnlineDepartments = query.Execute();

                //        foreach (Model.Department preloadDept in qrOnlineDepartments)
                //            existingOnlineDepartmentDictionary[preloadDept.Code] = preloadDept;
                //    }
                //    else
                //    {
                //        FileLogger.InformationEntry(
                //           "First Run of WebService - Skipping the Preloading of Departments.");
                //    }

                //    // Loop through the WebService Dept List and Create/ Update the Depts
                //    foreach (Department twDepartment in departmentsList)
                //    {
                //        currentTWDeptCode = twDepartment.Code;

                //        // Pull the Dept from the PreLoadedList
                //        Model.Department onlineDepartment = existingOnlineDepartmentDictionary.Keys.Contains(currentTWDeptCode)
                //                                                ? existingOnlineDepartmentDictionary[currentTWDeptCode]
                //                                                : null;
                //        if (onlineDepartment != null)
                //        {
                //            FileLogger.VerboseEntry(
                //                                 string.Format("Department ID {0} already exists for code {1}", onlineDepartment.Key.ID, currentTWDeptCode));
                //            onlineDepartment.Name = twDepartment.Name;
                //            FileLogger.VerboseEntry(string.Format("Updated Department '{0}' details.", onlineDepartment.Key.ID));
                //            updateCounter++;
                //        }
                //        else
                //        {
                //            FileLogger.VerboseEntry(
                //                                string.Format("Department does NOT exist for code {0}", currentTWDeptCode));
                //            // Create Department - since the Dept does not exist
                //            Department<DataObject> commonDept = new Department<DataObject>
                //            {
                //                AggregateSource = SourceTypes.TerryWhite,
                //                Name = twDepartment.Name,
                //                Code = twDepartment.Code
                //            };

                //            Model.Department newOnlineDepartment = Model.Department.Create(DataContext.Session, commonDept);
                //            FileLogger.VerboseEntry(
                //                                 string.Format("Department {0} Created for code {1}",
                //                                               newOnlineDepartment.Key.ID, currentTWDeptCode));
                //            insertCounter++;
                //        }
                //        // Update the sync time continuously.
                //        gds.TWDeptCategoryLastSync = DateTime.Now.AddMinutes(TW_DEPT_CAT_SYNC_INTRVL);
                //    }
                //    FileLogger.InformationEntry(string.Format("Completed Create/ Update Dept for {0} Creates and {1} updates", insertCounter, updateCounter));
                //    // Clear the variable once all the Depts are processed
                //    currentTWDeptCode = String.Empty;

                //    if (firstRun)
                //    {
                //        FileLogger.InformationEntry(
                //            "First Run of WebService - Skipping the Delete of Departments not supplied via WebService.");
                //    }
                //    else
                //    {
                //        // Identify the Existing FO Depts that does not exist in the New List and Remove Depts from DB
                //        FileLogger.InformationEntry(
                //            "Begin Processing Delete of Unwanted Departments");

                //        List<string> listExistingDepartments = existingOnlineDepartmentDictionary.Keys.ToList();
                //        List<string> listWebDepts = departmentsList.Select(dept => dept.Code).ToList();
                //        List<Key> deleteDepartmentList = new List<Key>();
                //        List<Key> deleteCatergoryList = new List<Key>();

                //        foreach (string code in listExistingDepartments)
                //        {
                //            // If the new Worksheet of Dept does not have any of the current Depts - Add to Delete List
                //            if (!listWebDepts.Contains(code))
                //            {
                //                Key deleteKey = existingOnlineDepartmentDictionary.ContainsKey(code)
                //                    ? existingOnlineDepartmentDictionary[code].Key
                //                    : new Key(0);

                //                if (deleteKey.ID > 0 && !deleteDepartmentList.Contains(deleteKey))
                //                    deleteDepartmentList.Add(deleteKey);
                //            }
                //        }
                //        // Get all the Categories from the Depts to be Deleted and add to Delete List
                //        foreach (var key in deleteDepartmentList)
                //        {
                //            Model.Department delDept = DataContext.Session[key] as Model.Department;
                //            if (delDept != null)
                //                deleteCatergoryList.AddRange(delDept.Categories.GetKeys());
                //        }
                //        FileLogger.InformationEntry(
                //            String.Format(
                //                "Identified {0} Departments and {1} Categories to be Deleted based on the latest Full Update Received",
                //                deleteDepartmentList.Count, deleteCatergoryList.Count));
                //        // last update before leaving this method
                //        gds.TWDeptCategoryLastSync = DateTime.Now.AddMinutes(TW_DEPT_CAT_SYNC_INTRVL);

                //        // Remove all the Depts not sent down along with the Categories of the Departments
                //        DataContext.Session.RemoveObjects(deleteCatergoryList.ToArray());
                //        // Now Delete the Dept to avoid unnecessary updates to Category Objext
                //        DataContext.Session.RemoveObjects(deleteDepartmentList.ToArray());

                //        FileLogger.InformationEntry(
                //            String.Format(
                //                "Successfully deleted {0} Departments and {1} Categories",
                //                deleteDepartmentList.Count, deleteCatergoryList.Count));
                //    }

                //    deptTransactions.Commit();
                //    FileLogger.InformationEntry(
                //                        String.Format(
                //                            "Successfully Committed the Transaction. All Changes Checked In."));
                //}
                //catch (Exception ex)
                //{
                //    // Empty Key represents that all the records are processed or none started.
                //    results.Add(CreateProcessResult(currentTWDeptCode,
                //                                    string.Format(
                //                                        "Error Occured in CreateUpdateDepartments. Aborting Transaction. No Deparments will be created/updated. {0} ",
                //                                        ex.Message)));
                //    FileLogger.ErrorEntry(
                //        "Error Occured in CreateUpdateDepartments. Aborting Transaction. No Deparments will be created/updated.",
                //        ex);
                //    deptTransactions.Rollback();
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                results.Add(CreateProcessResult(string.Format("Error Occured in CreateUpdateDepartments. Aborting Transaction. No Deparments will be created/updated. {0} ", ex.Message), ex.StackTrace));
                FileLogger.ErrorEntry(string.Format("Error Occured in CreateUpdateDepartments. Aborting Transaction. No Deparments will be created/updated. {0} ", ex.Message), ex);

            }
            finally
            {
                // Set the time back to now so that next update can run.
                //gds.TWDeptCategoryLastSync = DateTime.Now;
            }

            FileLogger.InformationEntry(string.Format("Successfully Completed the Create/Update Department Operation with {0} process results returned to the caller.", results.Count));
            return results;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProcessResult> CreateUpdateCategories(string userName, string password, List<Category> categoriesList)
        {
            List<ProcessResult> results = new List<ProcessResult>();
           
            try
            {
                // Check user name password
                FileLogger.InformationEntry(string.Format("Authenticating in CreateUpdateCategories"));
                AuthenticateWebServiceUser(userName, password);

                FileLogger.InformationEntry(string.Format("Authentication Successful. Received '{0}' of Categories to create or update", categoriesList.Count));

                #region FIXIT
                //DateTime lastSyncTime = gds.TWDeptCategoryLastSync;
                //FileLogger.InformationEntry(string.Format("Last TW Dept/Category sync time at the beginning: {0}", lastSyncTime));

                //if (lastSyncTime >= DateTime.Now)
                //{
                //    FileLogger.InformationEntry(string.Format("Last Sync time is not passed yet. LastTWDeptCategorySyncTime : {0}", lastSyncTime));
                //    results.Add(CreateProcessResult("CreateUpdateCategories", "Update to Departments and Categories is in progress now. Please try again in few minutes"));
                //    return results;
                //}

                //// Add 5 minutes interval so that incase of another client calling this method will be rejected in next 5 minutes
                //gds.TWDeptCategoryLastSync = DateTime.Now.AddMinutes(TW_DEPT_CAT_SYNC_INTRVL);
                //FileLogger.InformationEntry(string.Format("Last TW Dept/Category sync time before updating categories: {0}", gds.TWDeptCategoryLastSync));


                //// Truncate the department codes to max 50 before further process
                //foreach (Category category in categoriesList)
                //{
                //    category.Code = TruncateCode(category.Code);
                //    category.DepartmentCode = TruncateCode(category.DepartmentCode);
                //}

                //// We need to check for the Dept code anyway, so lets proceed with getting a list of Depts
                //Dictionary<string, Model.Department> existingOnlineDepartmentDictionary = new Dictionary<string, Model.Department>();
                //// Pull everything except the OtherCost Dept and the Default Dept
                //FileLogger.VerboseEntry("Getting all the departments except other cost and default ones.");
                //Query query = DataContext.Session.CreateQuery("select Department instances where {IsOtherCostDepartment} = 0 and {IsDefault} = 0 ");

                //QueryResult qrOnlineDepartments = query.Execute();

                //FileLogger.VerboseEntry(
                //                     "Preloading all the Departments to check for existing Dept Code before accepting Category Create/updates.");
                ////Preload the Objects
                //DataContext.Session.Preload(qrOnlineDepartments);
                //// construct a Dictionary with Dept Code as the Key and Dept as the Value
                //foreach (Model.Department loadDept in qrOnlineDepartments)
                //    existingOnlineDepartmentDictionary[loadDept.Code] = loadDept;

                //FileLogger.VerboseEntry("Finished Loading existing Departments. Current Dept Count is " + existingOnlineDepartmentDictionary.Count);
                //FileLogger.InformationEntry("Beginning to process Create/Update Request in CreateUpdateCategories");
                //// Category creation is similar to Dept. Its Either All In or All Out - even on one error.
                //// This loop is the first parse to identify the rogue records from the list supplied to us
                //foreach (Category twCategory in categoriesList)
                //{
                //    string processResultKey = twCategory.Code;
                //    FileLogger.VerboseEntry(
                //                         "Processing... Create/ Update for Category with Code - " + processResultKey);
                //    // If any of the value supplied is empty string - add to the result list and send it back to the caller
                //    if (twCategory.Code == String.Empty || twCategory.Name == String.Empty || twCategory.DepartmentCode == String.Empty)
                //    {
                //        string errorMessage =
                //            string.Format("Invalid Data Specified: Code - {0} , Name - {1}, Dept Code - {2}. Category can not be created/updated.",
                //                          twCategory.Code, twCategory.Name, twCategory.DepartmentCode);
                //        FileLogger.WarningEntry(errorMessage);
                //        results.Add(CreateProcessResult(processResultKey, errorMessage));
                //    }
                //    // If Dept code for category cannot be found in DB, bounce an error back  
                //    if (!existingOnlineDepartmentDictionary.Keys.Contains(twCategory.DepartmentCode))
                //    {
                //        string errorMessage =
                //            string.Format("Department with Dept Code - {0} cannot be found. Category with Code - {1}, Name -{2} can not be created/updated.",
                //                          twCategory.DepartmentCode, twCategory.Code, twCategory.Name);

                //        FileLogger.WarningEntry(errorMessage);
                //        results.Add(CreateProcessResult(processResultKey, errorMessage));
                //    }
                //}

                //if (results.Count > 0)
                //{
                //    string errorMsg = string.Format(
                //        "Aborting Category Creation. Error Occured in {0} records in the pre-check.",
                //        results.Count);
                //    FileLogger.InformationEntry(errorMsg);
                //    results.Add(CreateProcessResult(string.Empty, errorMsg));
                //    return results;
                //}

                //TransactionController catTransactions =
                //   DataContext.Session.CreateTransactionController(TransactionMode.TransactionRequired |
                //                                                   TransactionMode.Unsafe);
                //string currentTWCategoryDeptCode = String.Empty;
                //int updateCounter = 0;
                //int insertCounter = 0;
                //try
                //{

                //    // its guaranteed that the data is clean and all the Depts exist for the Categories to be updated/ created
                //    foreach (Category category in categoriesList)
                //    {
                //        currentTWCategoryDeptCode = category.DepartmentCode;
                //        //pull the dept first and check for category - if there update the category else create new category.
                //        Model.Department onlineDepartment = existingOnlineDepartmentDictionary[currentTWCategoryDeptCode];

                //        Model.Category selectedOnlineCategory =
                //            onlineDepartment.Categories.Cast<Model.Category>().FirstOrDefault(
                //                onlineCategory => onlineCategory.Code == category.Code);

                //        if (selectedOnlineCategory != null)
                //        {
                //            //update the category
                //            FileLogger.VerboseEntry(
                //                               string.Format("Category already exists for code {0}", currentTWCategoryDeptCode));
                //            selectedOnlineCategory.Name = category.Name;

                //            FileLogger.InformationEntry(
                //                               String.Format(
                //                                   "category details updated for Category ID {0}, code {1} , Name {2} in Department Code {3}",
                //                                   selectedOnlineCategory.Key.ID, category.Code, category.Name,
                //                                   category.DepartmentCode));
                //            updateCounter++;
                //        }
                //        else
                //        {
                //            //update the category
                //            FileLogger.VerboseEntry(
                //                               string.Format("Category does NOT exists for code {0} in Department Code {1}", currentTWCategoryDeptCode, category.DepartmentCode));

                //            Category<DataObject> commonCategory = new Category<DataObject>
                //            {
                //                Department = onlineDepartment,
                //                AggregateSource = SourceTypes.TerryWhite,
                //                Name = category.Name,
                //                Code = category.Code
                //            };

                //            Model.Category newCategory = Model.Category.Create(DataContext.Session, commonCategory);
                //            FileLogger.InformationEntry(
                //                                 String.Format(
                //                                     "A new category ID {0} is created for the code {1} , Name {2} in Department Code {3}",
                //                                     newCategory.Key.ID, category.Code, category.Name,
                //                                     category.DepartmentCode));
                //            insertCounter++;
                //        }

                //        // update sync time while creating/updating categories.
                //        gds.TWDeptCategoryLastSync = DateTime.Now.AddMinutes(TW_DEPT_CAT_SYNC_INTRVL);
                //    }

                //    FileLogger.InformationEntry(
                //                         string.Format(
                //                             "Completed Create/ Update Category for {0} Creates and {1} updates",
                //                             insertCounter, updateCounter));

                //    // Clear the variable once all the Categories are processed
                //    currentTWCategoryDeptCode = String.Empty;

                //    // Identify the Existing FO Depts that does not exist in the New List and Remove Depts from DB
                //    FileLogger.InformationEntry("Begin Processing Delete of Unwanted Categories");

                //    List<Key> deleteCatergoryList = new List<Key>();
                //    //loop through all the existing Fred Office Departments
                //    foreach (Model.Department onlineDepartment in existingOnlineDepartmentDictionary.Values)
                //    {
                //        // Loop through all the existing categories of the chosen department
                //        foreach (Model.Category onlineCategory in onlineDepartment.Categories)
                //        {
                //            // check if the TW CategoriesList has any matching entry for DeptCode and CatCode                           
                //            bool categoryFound = categoriesList.Any(
                //                twCategory => twCategory.Code == onlineCategory.Code
                //                && twCategory.DepartmentCode == onlineDepartment.Code);
                //            // if not then this category needs to be deleted
                //            if (!categoryFound)
                //                deleteCatergoryList.Add(onlineCategory.Key);
                //        }
                //    }


                //    FileLogger.InformationEntry(
                //                         String.Format(
                //                             "Identified {0} Categories to be Deleted based on the latest Full Update Received",
                //                             deleteCatergoryList.Count));
                //    // last update to sync time before completing this method.
                //    gds.TWDeptCategoryLastSync = DateTime.Now.AddMinutes(TW_DEPT_CAT_SYNC_INTRVL);

                //    // Remove all the Categoirs not sent down 
                //    DataContext.Session.RemoveObjects(deleteCatergoryList.ToArray());
                //    FileLogger.InformationEntry(String.Format("Successfully deleted {0} Categories. Committing the Transaction",
                //                      deleteCatergoryList.Count));

                //    // Commit the Transaction 
                //    catTransactions.Commit();
                //    FileLogger.InformationEntry(String.Format("Successfully Committed the Transaction. All Changes Checked In."));
                //}
                //catch (Exception ex)
                //{
                //    // Empty Key represents that all the records are processed or none started.
                //    results.Add(CreateProcessResult(currentTWCategoryDeptCode,
                //                                    string.Format("Error Occured in CreateUpdateCategories. Aborting Transaction. No Categories will be created/updated. {0} ",
                //                                        ex.Message)));

                //    FileLogger.ErrorEntry("Error Occured in CreateUpdateCategories. Aborting Transaction. No Categories will be created/updated.", ex);
                //    catTransactions.Rollback();
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                results.Add(CreateProcessResult(string.Format("Error Occured in CreateUpdateCategories. Aborting Transaction. No Categories will be created/updated. {0} ", ex.Message), ex.StackTrace));
                FileLogger.ErrorEntry(string.Format("Error Occured in CreateUpdateCategories. Aborting Transaction. No Categories will be created/updated. {0} ", ex.Message), ex);
            }
            finally
            {
                // Set the time back to now so that next update can run.
                //gds.TWDeptCategoryLastSync = DateTime.Now;
            }
            return results;
        }
        #endregion

        #region Product Promotion Services

        /// <summary>
        /// Gets the active campaign offer details
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<CampaignOfferHeader> GetActiveCampaignOfferHeaders(string userName, string password)
        {
            FileLogger.VerboseEntry(string.Format("In GetActiveCampaignOfferHeaders method"));
            FileLogger.InformationEntry("Web Service - Authenticating in GetActiveCampaignOfferHeaders");

            AuthenticateWebServiceUser(userName, password);
            FileLogger.InformationEntry("Web Service - Authentication Successful");

            List<CampaignOfferHeader> campaignOfferHeaders = new List<CampaignOfferHeader>();
            try
            {
                #region FIXIT
                //                string sqlQuery = string.Format(@"SELECT [doOfferCampaign].[Name] as CampaignName,
                //                                                [doOffer].[Code] as OfferCode,
                //                                                [doOffer].[Name] as OfferName,
                //                                                [doOffer].[Description] as OfferDescription,
                //                                                [doOffer].[OfferType] as OfferType,
                //                                                [doOffer].[IsLoyalty] as IsLoyalty,	
                //                                                [doOffer].[StartDate] as StartDate,	
                //                                                [doOffer].[EndDate] as EndDate,
                //                                                [doOffer].[DollarOffDiscount] as DollarOffDisc,
                //                                                [doOffer].[PercentOffDiscount] as PercentOffDisc,
                //                                                [doOffer].[DollarThreshold] as DollarThreshold,
                //                                                [doOffer].[MultiBuyXQuantity] as MultiBuyXQty,
                //                                                [doOffer].[MultiBuyYQuantity] as MultiBuyYQty,
                //                                                [doOffer].[MultiBuyYDollarAmount] as MultiBuyYDollarAmt,
                //                                                [doOffer].[QuantityThreshold] as QuantityThreshold,
                //                                                [doOffer].[DivideDiscount] as DivideDiscount
                //                                                FROM [doOfferCampaign] with (nolock)
                //                                                INNER JOIN [doOffer] with (nolock) 
                //	                                                on [doOffer].[Campaign] = [doOfferCampaign].[ID]
                //                                                WHERE [doOfferCampaign].[IsDefault] = 0 and [doOffer].[EndDate] >= GETDATE()");
                //                GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
                //                DataTable resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, null);
                //                FileLogger.VerboseEntry(string.Format("Results Retrieved for active campaign offer headers."));
                //                foreach (DataRow row in resultTable.Rows)
                //                {
                //                    campaignOfferHeaders.Add(new CampaignOfferHeader()
                //                    {
                //                        CampaignName = row.Field<string>("CampaignName"),
                //                        OfferCode = row.Field<string>("OfferCode"),
                //                        OfferName = row.Field<string>("OfferName"),
                //                        OfferDescription = row.Field<string>("OfferDescription"),
                //                        OfferType = row.Field<OfferType>("OfferType"),
                //                        IsLoyalty = row.Field<bool>("IsLoyalty"),
                //                        StartDate = row.Field<DateTime>("StartDate"),
                //                        EndDate = row.Field<DateTime>("EndDate"),
                //                        DollarOffDisc = row.Field<decimal>("DollarOffDisc"),
                //                        PercentOffDisc = row.Field<float>("PercentOffDisc"),
                //                        DollarThreshold = row.Field<float>("DollarThreshold"),
                //                        MultiBuyXQty = row.Field<int>("MultiBuyXQty"),
                //                        MultiBuyYQty = row.Field<int>("MultiBuyYQty"),
                //                        MultiBuyXDollarAmt = 0, // field to use it for the future
                //                        MultiBuyYDollarAmt = row.Field<decimal>("MultiBuyYDollarAmt"),
                //                        QuantityThreshold = row.Field<bool>("QuantityThreshold"),
                //                        DivideDiscount = row.Field<bool>("DivideDiscount"),
                //                    });
                //                } 
                #endregion
                FileLogger.VerboseEntry("Ready to return the List of Active Campaign Offer Details to Client");

            }
            catch (Exception ex)
            {
                FileLogger.ErrorEntry(
                                     "Error Occured getting the Active Campaign Offer Headers. Details - " + ex.Message,
                                     ex);
                throw;
            }
            FileLogger.InformationEntry(
                                 string.Format(
                                     "Successfully completed the Request in GetActiveCampaignOfferHeaders. Return header count: {0}",
                                     campaignOfferHeaders.Count));
            return campaignOfferHeaders;
        }


        /// <summary>
        /// Gets the offer entry details.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <param name="offerCodes">The offer codes.</param>
        /// <returns></returns>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<OfferEntry> GetOfferEntryDetails(string userName, string password, List<string> offerCodes)
        {
            FileLogger.VerboseEntry(string.Format("In GetOfferEntryDetail method for Offer Codes: {0}", offerCodes.Count));
            FileLogger.InformationEntry("Web Service - Authenticating in GetOfferEntryDetail");
            AuthenticateWebServiceUser(userName, password);
            FileLogger.InformationEntry("Web Service - Authentication Successful");

            List<OfferEntry> offerEntries = new List<OfferEntry>();
            if (offerCodes.Count == 0)
            {
                FileLogger.InformationEntry("No OfferCodes passed in. Returning empty offer entry list.");
                return offerEntries;
            }

#region FIXIT
		//            // Create parameter string for sql in query.
//            CustomDbParameter[] parameters = new CustomDbParameter[offerCodes.Count];
//            string[] parameterArray = new string[offerCodes.Count];
//            for (int i = 0; i < offerCodes.Count; i++)
//            {
//                parameterArray[i] = string.Format("@OfferCode{0}", i);
//                parameters[i] = new CustomDbParameter(parameterArray[i], DbType.String, offerCodes[i]);
//            }

//            try
//            {
//                string sqlQuery = string.Format(@"  -- Declare table variable to hold one numeric barcode for an item
//                        DECLARE @Barcodes TABLE(
//                         [ItemID] int,
//                         [Barcode] nvarchar(20) 
//                        )
//                        INSERT INTO @Barcodes (ItemID,Barcode)
//                        select [doAlias].[ItemAliased] ,max([doAlias].[Code]) as Barcode
//                        from [doAlias] with (nolock)
//                        inner join [doOfferEntry] with (nolock) on [doOfferEntry].[OfferItem]= [doAlias].[ItemAliased]
//                        inner join [doOffer] with (nolock) on [doOffer].[ID] = [doOfferEntry].[Offer]
//                        where [doOffer].[Code] in ({0}) and IsNumeric([doAlias].[Code]) = 1
//                        group by [doAlias].[ItemAliased]  
// 
//                        SELECT [doOfferCampaign].[Name] as CampaignName,
//                        [doOffer].[Code] as OfferCode,
//                        [doOffer].[OfferType] as OfferType,
//                        ISNULL(barcodes.Barcode,'') as Barcode,
//                        [doItemBase].[Description] as ProductName,
//                        [doOfferEntry].[Price] as OfferPrice,
//                        [doItemBase].[Price] as RetailPrice,
//                        [doItemBase].[QuantityOnHand] - [doItemBase].[QuantityCommitted] as QuantityAvailable,
//                        [doItemBase].[QuantityOnOrder],
//                        [doItemBase].[InActive] as Discontinued,
//                        [doItemBase].[LastSoldDate],
//                        [doItemBase].[Created] as CreatedDate,
//                        [doItem].[Detail1] as Brand,
//                        [doItem].[Detail2] as Name,
//                        [doItem].[Detail3] as Size
//                        FROM [doOfferCampaign] with (nolock)
//                        INNER JOIN [doOffer] with (nolock) 
//	                        on [doOffer].[Campaign] = [doOfferCampaign].[ID]
//                        INNER JOIN [doOfferEntry] with (nolock)
//	                        on [doOfferEntry].[Offer] = [doOffer].[ID]
//                        INNER JOIN [doItemBase] with (nolock)
//	                        on [doItemBase].[ID] = [doOfferEntry].[OfferItem]
//                        INNER JOIN [doItem] with (nolock)
//	                        on [doItem].[ID] = [doOfferEntry].[OfferItem]
//                        LEFT OUTER JOIN @Barcodes  barcodes on [barcodes].[ItemID]  = [doItemBase].[ID]
//                        WHERE [doOffer].[Code] in ({0})", string.Join(", ", parameterArray));

//                GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
//                DataTable resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, parameters);
//                FileLogger.VerboseEntry(string.Format("Results Retrieved for Offer entries for Offer Codes: {0}", offerCodes.Count));

//                foreach (DataRow row in resultTable.Rows)
//                {
//                    OfferType offerType = (OfferType)row.Field<int>("OfferType");
//                    offerEntries.Add(new OfferEntry
//                    {
//                        CampaignName = row.Field<string>("CampaignName"),
//                        OfferCode = row.Field<string>("OfferCode"),
//                        EAN = row.Field<string>("Barcode"),
//                        ProductName = row.Field<string>("ProductName"),
//                        OfferPrice =
//                            (offerType == OfferType.Set_Advertised_Price ||
//                             offerType == OfferType.Buy_Down_Offer)
//                                ? row.Field<decimal>("OfferPrice")
//                                : 0m,
//                        RetailPrice = row.Field<decimal>("RetailPrice"),
//                        QuantityAvailable = row.Field<double>("QuantityAvailable"),
//                        QuantityOnOrder = row.Field<double>("QuantityOnOrder"),
//                        Discontinued = row.Field<bool>("Discontinued"),
//                        LastSoldDate = row.Field<DateTime>("LastSoldDate"),
//                        CreatedDate = row.Field<DateTime>("CreatedDate"),
//                        Brand = row.Field<string>("Brand"),
//                        Name = row.Field<string>("Name"),
//                        Size = row.Field<string>("Size"),
//                    });

//                } 
	

            //    FileLogger.VerboseEntry("Ready to return the List of Offer Entries to Client");
            //}
            //catch (Exception ex)
            //{
            //    FileLogger.ErrorEntry(
            //                         "Error Occured getting the Offer Entry Details. Details - " + ex.Message,
            //                         ex);
            //    throw;
            //}
    #endregion
            FileLogger.InformationEntry(string.Format("Successfully completed the Request in GetOfferEntryDetails. Return offer entry count: {0}", offerEntries.Count));
            return offerEntries;
        }

        #endregion

        #region Product Pricing Services
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProductPricing> GetProductPricingDetails(string userName, string password, List<string> productHQIDs)
        {
            FileLogger.VerboseEntry(
                                 string.Format("In GetProductPricingDetails method for Product HQIDs Count: {0}", productHQIDs.Count));
            FileLogger.InformationEntry(
                                 "Web Service - Authenticating in GetProductPricingDetails");
            AuthenticateWebServiceUser(userName, password);
            FileLogger.InformationEntry("Web Service - Authentication Successful");

            List<ProductPricing> productPricingList = new List<ProductPricing>();
            if (productHQIDs.Count == 0)
            {
                FileLogger.InformationEntry(
                                     "Empty List of Product HQIDs passed. Returning Empty List Back");
                return productPricingList;
            }

            try
            {
                FileLogger.VerboseEntry("Constructing XML for Get Product Pricing query");
                StringWriter writer = new StringWriter();
                XmlTextWriter xmlWriter = new XmlTextWriter(writer);

                xmlWriter.WriteStartElement("Preloads");

                // Loop through all rows grabbing out HeadOfficeIDs that need to be matched
                foreach (string HeadOfficeID in productHQIDs)
                {
                    xmlWriter.WriteStartElement("P");
                    xmlWriter.WriteAttributeString("C", HeadOfficeID);
                    xmlWriter.WriteEndElement();
                }
                FileLogger.InformationEntry("Begin Query to get Product Pricing Details.");

                xmlWriter.WriteEndElement();
                xmlWriter.Flush();

//                CustomDbParameter[] parameters = new CustomDbParameter[1];
//                parameters[0] = new CustomDbParameter("@Xml", writer.ToString());



//                string sqlQuery = String.Format(@"                 DECLARE @doc int 
//                        EXEC sp_xml_preparedocument @doc OUTPUT, @Xml
//                        -- Declare the Table Variables
//                        DECLARE @Item TABLE (ItemHQId bigint PRIMARY KEY)
//						DECLARE @ItemPricing TABLE (ID bigint PRIMARY KEY,ProductHQID bigint, ItemCode nvarchar(25),
//													Description nvarchar(50),Brand nvarchar(50),
//                                                    Name nvarchar(50),Size nvarchar(50),
//													ItemLocation nvarchar(50),PricingPolicy nvarchar(50),
//													LastPurchaseCost decimal(10,2),CurrentPrice decimal(10,2),
//													StockOnHand float,InActive bit,LastPurchaseDate DateTime,
//													LastSoldDate DateTime,CreatedDate DateTime )
//
//                        DECLARE @ItemPartial TABLE (	ID bigint PRIMARY KEY,	CurrentOfferEntryID bigint)	
//
//						DECLARE @ItemPromotion TABLE (	ID bigint PRIMARY KEY,				
//														CurrentSalePrice decimal(10,2),
//														IsOnCurrentPromotion bit,
//														DaysToNextPromotion int )													  
//													  
//                        INSERT INTO @Item ( ItemHQId )
//                        SELECT C FROM OPENXML(@doc, N'/Preloads/P') 
//                        WITH (C int)		
//
//						insert into @ItemPricing ( ID, ProductHQID, ItemCode, Description,
//													Brand, Name, Size, ItemLocation, PricingPolicy,
//													LastPurchaseCost, CurrentPrice, StockOnHand,
//                                                    InActive, LastPurchaseDate, LastSoldDate, CreatedDate)
//                        select doItemBase.ID, HeadOfficeID, doItemBase.Code, Description, 
//                        Detail1, Detail2, Detail3, ItemLocation, PricingPolicy ,
//                        Cost, Price, convert(decimal(5,1),QuantityOnHand), 
//                        InActive, LastOrdered,LastSoldDate, Created 					
//                        from doItemBase with (nolock)                                                            
//                        Inner Join doItem with (nolock)
//                            on doItemBase.ID = doItem.ID
//                        inner join @Item as Item 
//                            on doItem.HeadOfficeID = Item.ItemHQId    
//
//                        insert into @ItemPartial (ID, CurrentOfferEntryID)
//				        SELECT currentOfferEntry.OfferItem, min(isnull(currentOfferEntry.ID, 0)) as CurrentOffer
//				        From @ItemPricing ItemPricing				
//				        inner join doOfferEntry currentOfferEntry
//					        on ItemPricing.ID = currentOfferEntry.OfferItem
//				        inner join doOffer currentOffer
//					        on currentOfferEntry.Offer = currentOffer.ID
//					        and currentOffer.StartDate <= GETDATE() 
//					        and currentOffer.EndDate >= GETDATE()
//				        group by currentOfferEntry.OfferItem
//    
//                            
//                         insert into @ItemPromotion (ID, CurrentSalePrice, IsOnCurrentPromotion, DaysToNextPromotion)
//                         select ItemPricing.ID, 
//                                -- If the Offer Type is 0 Set Price or 7 Buy Down return the Sale Price from OfferEntry
//						        -- For the rest of the Offer Types return 0.00. Min is just to avoid Grouping
//						        case	when min(isnull(currentOffer.OfferType, 99)) = 0 or min(isnull(currentOffer.OfferType, 99)) = 7 
//								        then   min(isnull(currentOfferEntry.Price, 0)) 
//								        else 0.00 
//						        end		as CurrentSalePrice,
//                                 -- this is already the current offer. If offer isnt null then item is on Promo 
//								case	when min(isnull(currentOffer.StartDate, getdate() + 1 )) <= getdate()
//										then 1 else 0
//								end		as IsOnCurrentPromotion,
//							 case	when max(isnull(nextOffer.StartDate, getdate())) <= getdate()
//				                    then 0
//				                    else Datediff(d, getdate(), MAX(isnull(nextOffer.StartDate, getdate())))
//								end as DaysToNextPromo						 
//                         from @ItemPricing ItemPricing
//						 left join @ItemPartial ItemPartial
//					        on ItemPartial.ID = ItemPricing.ID
//				         left join doOfferEntry CurrentOfferEntry
//					        on ItemPartial.CurrentOfferEntryID = CurrentOfferEntry.ID 
//				         left join doOffer CurrentOffer
//					        on CurrentOfferEntry.Offer = CurrentOffer.ID 
//						 left join doOfferEntry nextOfferEntry
//							on ItemPricing.ID = nextOfferEntry.OfferItem
//						 left join doOffer nextOffer
//							on nextOfferEntry.Offer = nextOffer.ID
//							and nextOffer.StartDate >= GETDATE() 
//							and nextOffer.EndDate >= GETDATE()
//                           Group by ItemPricing.ID
//                           
//                           select	ItemPricing.*, ItemPromotion.CurrentSalePrice,
//									ItemPromotion.IsOnCurrentPromotion,
//									ItemPromotion.DaysToNextPromotion
//                           from @ItemPricing ItemPricing
//                           inner join @ItemPromotion ItemPromotion
//                           on ItemPricing.ID = ItemPromotion.ID         ");
//                FileLogger.VerboseEntry(string.Format("Ready witht the query {0} ", sqlQuery));
//                GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
//                DataTable resultTable = generalDataService.ExecuteTableQuery(sqlQuery, 600, parameters);
                FileLogger.VerboseEntry(string.Format("Results Retrieved for Get Product Pricing for HeadOfficeIDs: {0}", productHQIDs.Count));
                DataTable resultTable = new DataTable();
                productPricingList.AddRange(from DataRow row in resultTable.Rows
                                            select new ProductPricing
                                            {
                                                ItemCode = row.Field<string>("ItemCode"),
                                                Description = row.Field<string>("Description"),
                                                ProductHQID = row.Field<long>("ProductHQID"),
                                                Brand = row.Field<string>("Brand"),
                                                Name = row.Field<string>("Name"),
                                                Size = row.Field<string>("Size"),
                                                ItemLocation = row.Field<string>("ItemLocation"),
                                                CurrentPricingPolicy =
                                                    row.Field<string>("PricingPolicy"),
                                                LastPurchaseCost = row.Field<decimal>("LastPurchaseCost"),
                                                CurrentPrice = row.Field<decimal>("CurrentPrice"),
                                                StockOnHand = row.Field<double>("StockOnHand"),
                                                InActive = row.Field<bool>("InActive"),
                                                LastPurchaseDate = row.Field<DateTime>("LastPurchaseDate"),
                                                DaysToNextPromotion = row.Field<int>("DaysToNextPromotion"),
                                                CurrentSalePrice = row.Field<decimal>("CurrentSalePrice"),
                                                IsOnCurrentPromotion = row.Field<bool>("IsOnCurrentPromotion"),
                                                LastSoldDate = row.Field<DateTime>("LastSoldDate"),
                                                CreatedDate = row.Field<DateTime>("CreatedDate")
                                            });

                FileLogger.VerboseEntry("Ready to return the List of Product Pricing Details back to Client");
            }
            catch (Exception ex)
            {
                FileLogger.ErrorEntry(
                                     "Error Occured getting the Product Pricing Details. Details - " + ex.Message,
                                     ex);
                throw;
            }
            FileLogger.InformationEntry(
                                 "Successfully completed the Request in GetProductPricingDetails.Return List Count" + productPricingList.Count);

            return productPricingList;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProcessResult> SetProductPricingDetails(string userName, string password, List<NewProductPricing> productPricingDetails)
        {
            List<ProcessResult> processErrorList = new List<ProcessResult>();
            FileLogger.InformationEntry(
                                 "Web Service - Authenticating in SetProductPricingDetails");
            AuthenticateWebServiceUser(userName, password);
            FileLogger.InformationEntry("Web Service - Authentication Successful");
            try
            {
                FileLogger.VerboseEntry(
                               string.Format("In SetProductPricingDetails method for ProductPricingDetails Codes: {0}", productPricingDetails.Count));
                if (productPricingDetails.Count == 0)
                {
                    string errorMessage = string.Format(
                           "Empty List Supplied. Cannot Update Item Pricing Details.");
                    FileLogger.InformationEntry(errorMessage);
                    CreateProcessError(String.Empty, processErrorList, errorMessage);
                    // Do not proceed - since there is nothing to be done anyway
                    return processErrorList;
                }

                FileLogger.InformationEntry(
                                 "Processing the SetProductPricingDetails for " + productPricingDetails.Count);

                foreach (NewProductPricing newProductPricing in productPricingDetails)
                {
                    string processResultKey = "Item HQID : " + newProductPricing.ProductHQID;
                    try
                    {
                        FileLogger.VerboseEntry(
                                             "Processing... SetProductPricing for Item with HQID- " +
                                             newProductPricing.ProductHQID);

                        // If the Item Cannot be found for the HeadOffice HQID , record ProcessResultand continue to next item
                        if (newProductPricing.ProductHQID <= 0)
                        {
                            string errorMessage = string.Format(
                                "Invalid  Item HQID: {0}. Item Pricing Detail cannot be updated.",
                                newProductPricing.ProductHQID);
                            FileLogger.InformationEntry(errorMessage);
                            CreateProcessError(processResultKey, processErrorList, errorMessage);
                            continue;
                        }

                        #region FIXIT
                        //GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();

                        //long itemId = GetItemByHQID(newProductPricing.ProductHQID, generalDataService);

                        //if (itemId > 0)
                        //{
                        //    Item item = DataContext.Session[new Key(itemId)] as Item;
                        //    if (item != null)
                        //    {
                        //        FileLogger.VerboseEntry(
                        //                             string.Format(
                        //                                 "Updating Pricing for item with data object ID '{0}'", itemId));
                        //        item.Price = GlobalValues.Money(newProductPricing.NewPrice);
                        //        item.PricingPolicy = newProductPricing.OverridingPricingPolicy;

                        //        FileLogger.VerboseEntry(
                        //                             string.Format("Flag to remove Item {0} from Pricing Plan is {1}",
                        //                                           item.Key.ID, newProductPricing.RemoveFromPricingPlan));

                        //        if (newProductPricing.RemoveFromPricingPlan)
                        //            item.PricingPlan = null;
                        //        FileLogger.VerboseEntry(
                        //                             string.Format(
                        //                                 "Finished Updating Pricing for item with data object ID '{0}'",
                        //                                 itemId));
                        //    }
                        //    else
                        //    {
                        //        string errorMessage = string.Format(
                        //            "Item cannot be loaded for Item HQID: {0}. Item Pricing Detail cannot be updated.",
                        //            newProductPricing.ProductHQID);
                        //        FileLogger.InformationEntry(errorMessage);
                        //        CreateProcessError(processResultKey, processErrorList, errorMessage);
                        //    }
                        //}
                        //else
                        //{
                        //    string errorMessage = string.Format(
                        //        "Item with Item HQID: {0} cannot be found. Item Pricing Detail cannot be updated.",
                        //        newProductPricing.ProductHQID);
                        //    FileLogger.InformationEntry(errorMessage);
                        //    CreateProcessError(processResultKey, processErrorList, errorMessage);
                        //} 
                        #endregion
                    }
                    catch (Exception ex)
                    {
                        string errorMessage =
                            string.Format(
                                "Error Occured in Item Pricing Update for {0}. Item Pricing Detail cannot be updated. Details - " +
                                ex.Message, processResultKey);
                        FileLogger.ErrorEntry(errorMessage, ex);
                        CreateProcessError(processResultKey, processErrorList, errorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                string errorMessage = "Error Occured Setting the Product Pricing Details. Details - " + ex.Message;
                FileLogger.ErrorEntry(errorMessage, ex);
                CreateProcessError(string.Empty, processErrorList, errorMessage);
            }
            return processErrorList;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
        public List<ProcessResult> QueueLabels(string userName, string password, List<LabelQueue> labelQueue)
        {
            List<ProcessResult> processErrorList = new List<ProcessResult>();
            FileLogger.InformationEntry(
                                "Web Service - Authenticating in QueueLabels");
            AuthenticateWebServiceUser(userName, password);
            FileLogger.InformationEntry("Web Service - Authentication Successful");
            try
            {
                FileLogger.VerboseEntry(
                          string.Format("In QueueLabels method for Total of {0} Labels", labelQueue.Count));
                if (labelQueue.Count == 0)
                {
                    string errorMessage = string.Format(
                           "Empty Label Queue List Supplied. Cannot create Label Queue.");
                    FileLogger.InformationEntry(errorMessage);
                    CreateProcessError(String.Empty, processErrorList, errorMessage);
                    // Do not proceed - since there is nothing to be done anyway
                    return processErrorList;
                }

                FileLogger.InformationEntry(
                                 String.Format("Processing the Label Queue for {0} labels", labelQueue.Count));
                #region FIXIT

                //GeneralDataService generalDataService = DataContext.Session.GetService<GeneralDataService>();
                //LabelPrintingDataService labelPrintingDataService =
                //    DataContext.Session.GetService<LabelPrintingDataService>();

                //foreach (LabelQueue productLabel in labelQueue)
                //{
                //    string processResultKey = "Item HQID : " + productLabel.ProductHQID;
                //    FileLogger.VerboseEntry(
                //                         string.Format("Processing Label Queue for the Item {0}", processResultKey));
                //    try
                //    {
                //        long itemID = GetItemByHQID(productLabel.ProductHQID, generalDataService);
                //        if (itemID > 0)
                //        {
                //            FileLogger.VerboseEntry(
                //                         string.Format("Item Exists. Ready to Create/Update Label Queue for ID {0}", itemID));
                //            // Passing Null Template would sort out the Item Template ?? Default Template.
                //            // If Dual Location is TRUE - LabelTemplate will be set to Dual Location
                //            labelPrintingDataService.CreateUpdateLabelQueue(new Key(itemID), LabelQueueSourceType.SignIQ,
                //                                                            null, productLabel.EffectiveDate,
                //                                                            DateTime.Now, productLabel.Description,
                //                                                            productLabel.LabelPrice,
                //                                                            productLabel.LabelQuantity,
                //                                                            productLabel.IsDualLocation);
                //            FileLogger.VerboseEntry("Label Queue Created/updated sucessfully");
                //        }
                //        else
                //        {
                //            string errorMessage =
                //          string.Format(
                //              "Item Not Found for {0}. Product Label cannot be queued.", processResultKey);
                //            FileLogger.WarningEntry(errorMessage);
                //            CreateProcessError(processResultKey, processErrorList, errorMessage);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        string errorMessage =
                //            string.Format(
                //                "Error Occured in Queue Label for {0}. Product Label cannot be queued. Details - " +
                //                ex.Message, processResultKey);
                //        FileLogger.ErrorEntry(errorMessage, ex);
                //        CreateProcessError(processResultKey, processErrorList, errorMessage);
                //    }
                //} 
                #endregion
            }
            catch (Exception ex)
            {
                string errorMessage = "Error Occured Queuing the Labels. Details - " + ex.Message;
                FileLogger.ErrorEntry(errorMessage, ex);
                CreateProcessError(string.Empty, processErrorList, errorMessage);
            }
            return processErrorList;
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Authenticate the Credentials trying to use the Integration Service
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        private static void AuthenticateWebServiceUser(string userName, string password)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                FileLogger.ErrorEntry("Error Occured Authenticating the User -" + userName, ex);
                throw new Exception("Authentication failed for the supplied credentials. Request Operation cannot be processed. Details - " + ex.Message);
            }            
        }
        
        private void CreateProcessError(string processResultKey, List<ProcessResult> processErrorList, string errorMessage)
        {
            FileLogger.WarningEntry(errorMessage);
            processErrorList.Add(CreateProcessResult(processResultKey, errorMessage));
        }             
        
        /// <summary>
        /// Creates Process Result
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="hasError">if set to <c>true</c> [has error].</param>
        /// <returns></returns>
        private ProcessResult CreateProcessResult(string key, string errorMessage, bool hasError = true)
        {
            return new ProcessResult { Key = key, Message = errorMessage, HasError = hasError };
        }

        /// <summary>
        /// Determines whether [is null or white space] [the specified value].
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>
        /// 	<c>true</c> if [is null or white space] [the specified value]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsNullOrWhiteSpace(string value)
        {
            if (value == null) return true;
            return string.IsNullOrEmpty(value.Trim());
        }

        /// <summary>
        /// Gets the truncated code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns></returns>
        private string TruncateCode(string code)
        {
            // Log the code with length more than 50 characters length that is going to be truncated.
            if (!string.IsNullOrEmpty(code) && code.Length > TW_DEPT_CAT_CODE_MAX_LENGTH)
            {
                FileLogger.VerboseEntry(string.Format("Long Code:'{0}' will be truncated to {1} characters and used in further process.", code, TW_DEPT_CAT_CODE_MAX_LENGTH));
            }
            return string.IsNullOrEmpty(code)
                       ? string.Empty
                       : code.Substring(0, Math.Min(code.Length, TW_DEPT_CAT_CODE_MAX_LENGTH));
        }

        #endregion
    }

    /// <summary>
    /// SupplierItem
    /// </summary>
    public class SupplierProduct
    {
        public long ProductHQID { get; set; }
        public long SupplierHQID { get; set; }
        public bool PreferredSupplier { get; set; }
        public bool SetPreferredAsPrimary { get; set; }
        public int PackSize { get; set; }
        public string ReorderNumber { get; set; }
        public decimal SupplierRRP { get; set; }
        public int MinOrderQty { get; set; }
        public decimal SupplierCost { get; set; }
    }

    /// <summary>
    /// Product
    /// </summary>
    public class Product
    {
        public Product()
        {
            // Defaults if client doesn't supply these values.
            ProductHQID = 0;
            Description = string.Empty;
            ProductType = ProductType.Standard;
            HasPurchaseTax = true;
            HasSalesTax = true;
            Aliases = new List<string>();
            TWManaged = true;
            DepartmentCode = string.Empty;
            CategoryCode = string.Empty;
            ManufacturerHQID = 0;
            ItemLocation = string.Empty;
            Brand = string.Empty;
            Name = string.Empty;
            Size = string.Empty;
            Discontinued = false;
            UpdateDescription = false;
            UpdateTaxes = false;
            UpdateItemLocation = false;
            AppendBarcodes = false;
        }

        public long ProductHQID { get; set; }
        public string Description { get; set; }
        public ProductType ProductType { get; set; }
        public bool HasPurchaseTax { get; set; }
        public bool HasSalesTax { get; set; }
        public List<string> Aliases { get; set; }
        public bool TWManaged { get; set; }
        public string DepartmentCode { get; set; }
        public string CategoryCode { get; set; }
        public long ManufacturerHQID { get; set; }
        public string ItemLocation { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public bool Discontinued { get; set; }
        public bool UpdateDescription { get; set; }
        public bool UpdateTaxes { get; set; }
        public bool UpdateItemLocation { get; set; }
        public bool AppendBarcodes { get; set; }
    }

    public class Department
    {
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class Category
    {
        public string DepartmentCode { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    /// <summary>
    /// Process Result
    /// </summary>
    public class ProcessResult
    {
        public string Key;
        public bool HasError;
        public string Message;
    }

    /// <summary>
    /// Product Type
    /// </summary>
    public enum ProductType
    {
        Standard = 0,
        Non_Inventory = 5
    }

    /// <summary>
    /// Offer Entry
    /// </summary>
    public class OfferEntry
    {
        public string CampaignName { get; set; }
        public string OfferCode { get; set; }
        public string EAN { get; set; }
        public string ProductName { get; set; }
        public decimal OfferPrice { get; set; }
        public decimal RetailPrice { get; set; }
        public double QuantityAvailable { get; set; }
        public double QuantityOnOrder { get; set; }
        public bool Discontinued { get; set; }
        public DateTime LastSoldDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
    }

    /// <summary>
    /// Product Pricing details
    /// </summary>
    public class ProductPricing
    {
        public long ProductHQID { get; set; }
        public string ItemCode { get; set; }
        public string Description { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Size { get; set; }
        public string ItemLocation { get; set; }
        public string CurrentPricingPolicy { get; set; }
        public decimal LastPurchaseCost { get; set; }
        public decimal CurrentPrice { get; set; }
        public double StockOnHand { get; set; }
        public bool InActive { get; set; }
        public DateTime LastPurchaseDate { get; set; }
        public DateTime LastSoldDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public int DaysToNextPromotion { get; set; }
        public decimal CurrentSalePrice { get; set; }
        public bool IsOnCurrentPromotion { get; set; }
    }

    /// <summary>
    /// New Pricing for the product
    /// </summary>
    public class NewProductPricing
    {
        public long ProductHQID { get; set; }
        public decimal NewPrice { get; set; }
        public string OverridingPricingPolicy { get; set; }
        public bool RemoveFromPricingPlan { get; set; }
    }

    /// <summary>
    /// Campaign Offer Header
    /// </summary>
    public class CampaignOfferHeader
    {
        public string CampaignName { get; set; }
        public string OfferCode { get; set; }
        public string OfferName { get; set; }
        public string OfferDescription { get; set; }
        public OfferType OfferType { get; set; }
        public bool IsLoyalty { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal DollarOffDisc { get; set; }
        public float PercentOffDisc { get; set; }
        public float DollarThreshold { get; set; }
        public int MultiBuyXQty { get; set; }
        public int MultiBuyYQty { get; set; }
        public decimal MultiBuyXDollarAmt { get; set; }
        public decimal MultiBuyYDollarAmt { get; set; }
        public bool QuantityThreshold { get; set; }
        public bool DivideDiscount { get; set; }
    }

    public class LabelQueue
    {
        public LabelQueue()
        {
            LabelQuantity = 1;
            EffectiveDate = DateTime.Now;
        }

        public long ProductHQID { get; set; }
        public string Description { get; set; }
        public int LabelQuantity { get; set; }
        public decimal LabelPrice { get; set; }
        public DateTime EffectiveDate { get; set; }
        public bool IsDualLocation { get; set; }
    }

    public enum OfferType
    {
        DateRangeOffer = 0,
        BuyDownOffer = 1
    }
}
