using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace TastyFoods
{
    public static class SEO
    {
        //public static string RootDomain = "http://www.TastyFoods.com.au";
             
        public static string RootDomain = "http://localhost/tastyfoods";

        #region - Definition -

        #region - Canonical -

        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_CANONICAL = "home-of-traditional-sri-lankan-and-indian-restaurant-in-melbourne";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_CANONICAL = "about-our-traditional-sri-lankan-and-indian-cuisine-in-melbourne";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_CANONICAL = "gallery-of-traditional-sri-lankan-and-indian-cuisine-in-melbourne";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_CANONICAL = "contact-our-traditional-sri-lankan-and-indian-restaurant";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_CANONICAL = "sorry-page-not-found";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_CANONICAL = "menu-of-traditional-sri-lankan-and-indian-cuisine-in-melbourne";

        public const string MENU_CHEFS_SPECIAL_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_REGULAR_DISHES_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_CURRY_DISHES_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_VEGETARIAN_DISHES_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_SALADS_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_DEVIL_DISHES_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_CHICKEN_SPECIAL_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_RICE_DISHES_CANONICAL = MENU_INDEX_CANONICAL;
        public const string MENU_SWEET_CANONICAL = MENU_INDEX_CANONICAL;

        #endregion

        #region - Url -

        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_URL = "home-of-traditional-sri-lankan-and-indian-restaurant-in-melbourne";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_URL = "about-our-traditional-sri-lankan-and-indian-cuisine-in-melbourne";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_URL = "gallery-of-traditional-sri-lankan-and-indian-cuisine-in-melbourne";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_URL = "contact-our-traditional-sri-lankan-and-indian-restaurant";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_URL = "sorry-page-not-found";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_URL = "menu-of-traditional-sri-lankan-and-indian-cuisine-in-melbourne";

        public const string MENU_CHEFS_SPECIAL_URL = MENU_INDEX_URL + "#chefs-special";
        public const string MENU_REGULAR_DISHES_URL = MENU_INDEX_URL + "#regular-dishes";
        public const string MENU_CURRY_DISHES_URL = MENU_INDEX_URL + "#curry-dishes";
        public const string MENU_VEGETARIAN_DISHES_URL = MENU_INDEX_URL + "#vegetarian-dishes";
        public const string MENU_SALADS_URL = MENU_INDEX_URL + "#salads";
        public const string MENU_DEVIL_DISHES_URL = MENU_INDEX_URL + "#egg-chicken-and-beef-devil-dishes";
        public const string MENU_CHICKEN_SPECIAL_URL = MENU_INDEX_URL + "#chicken-special";
        public const string MENU_RICE_DISHES_URL = MENU_INDEX_URL + "#rice-dishes";
        public const string MENU_SWEET_URL = MENU_INDEX_URL + "#sweets-and-dessert";

        #endregion


        #region - Title -

        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_TITLE = "Home of traditional Sri Lankan and Indian restaurant in Melbourne";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_TITLE = "About our traditional Sri Lankan and Indian cuisine in Melbourne";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_TITLE = "Gallery of traditional Sri Lankan and Indian cuisine in Melbourne";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_TITLE = "Contact our traditional Sri Lankan and Indian restaurant";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_TITLE = "Page not found";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_TITLE = "Menu of traditional Sri Lankan and Indian cuisine in Melbourne";

        public const string MENU_CHEFS_SPECIAL_TITLE = "Chef's special menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_REGULAR_DISHES_TITLE = "Regular menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_CURRY_DISHES_TITLE = "Curry menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_VEGETARIAN_DISHES_TITLE = "Vegetarian menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_SALADS_TITLE = "Salad menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_DEVIL_DISHES_TITLE = "Devil menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_CHICKEN_SPECIAL_TITLE = "Chicken menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_RICE_DISHES_TITLE = "Rice menu of traditional Sri Lankan and Indian cuisine";
        public const string MENU_SWEET_TITLE = "Sweet & dessert menu of traditional Sri Lankan and Indian cuisine";

        #endregion


        #region - Description -


        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_DESCRIPTION = "Experience the traditional authentic taste of Sri Lankan and Indian cuisine in Melbourne. Dine-in or take-away our chef's special lunch and dinner menu.";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_DESCRIPTION = "We specialise in authentic Sri Lankan and Indian recipes to make you feel at home back in Sri Lanka or in India with our home made style chef's specials";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_DESCRIPTION = "Our chef's special includes wide range of Sri Lankan cuisine & Indian spicy food prepared with traditional taste & style when your order and served fresh";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_DESCRIPTION = "Visit our restaurant for daily chef's special for lunch and dinner and enjoy our traditional cuisine. We are open 7 days between 10:00 am till 9:00 pm.";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_DESCRIPTION = "We're sorry. Something went wrong. We are working to fix the problem. Try again later.";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_DESCRIPTION = "If you missed your home food in Melbourne, then our chef's special menu is for you to enjoy the traditional authentic taste of Sri Lankan and Indian cuisine";

        public const string MENU_CHEFS_SPECIAL_DESCRIPTION = "Our chef's special is to surprise our hungry little friends by changing our menu from top to bottom every day giving a wider choice of lunch and dinner menu";
        public const string MENU_REGULAR_DISHES_DESCRIPTION = "Our regular menu includes starters, vegetarian, non-vegetarian dishes and dessert made with home ground spices to provide home-style lunch and dinner";
        public const string MENU_CURRY_DISHES_DESCRIPTION = "Chicken, mutton, beef, fish, crab, squid and egg curries are always on our chef's special list for you to enjoy the traditional home-style lunch and dinner";
        public const string MENU_VEGETARIAN_DISHES_DESCRIPTION = "Our home-style delicious vegetarian dishes are prepared with fresh vegetables, grams and home ground spices to add traditional Sri Lankan & Indian taste";
        public const string MENU_SALADS_DESCRIPTION = "Carefully selected vegetables, meat and dressings adds a touch of English flavour to our traditional Sri Lankan and Indian home-style delicious dishes";
        public const string MENU_DEVIL_DISHES_DESCRIPTION = "Chicken, beef and egg devil dishes are the most talked about items in our range of chef's special menu with authentic taste and traditional flavours";
        public const string MENU_CHICKEN_SPECIAL_DESCRIPTION = "Our chicken special includes tandoori, chicken 65, chicken fry dishes and range of special chicken curries with home-style traditional & authentic taste";
        public const string MENU_RICE_DISHES_DESCRIPTION = "Our chef's special rice of fried rice, vegetable rice, yoghurt rice and much more are to surprise our hungry friends during lunch & dinner time";
        public const string MENU_SWEET_DESCRIPTION = "Our traditional sweet and dessert menu is made with love and care to ensure quality and authenticity of Sri Lankan and Indian traditional taste and flavours";


        #endregion


        #region - Keywords Definition -


        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";

        public const string MENU_CHEFS_SPECIAL_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_REGULAR_DISHES_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_CURRY_DISHES_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_VEGETARIAN_DISHES_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_SALADS_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_DEVIL_DISHES_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_CHICKEN_SPECIAL_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_RICE_DISHES_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";
        public const string MENU_SWEET_KEYWORDS = "Sri Lankan Restaurant, Indian Restaurant, Lunch, Dinner, Curry, Biryani, Chicken Curry, Beef Curry, Fish Curry, Rice and Curry, Fried Rice, Sweet";

        #endregion


        #region - Header -


        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_HEADER = "XXX";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_HEADER = "XXX";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_HEADER = "XXX";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_HEADER = "XXX";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_HEADER = "XXX";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_HEADER = "XXX";

        public const string MENU_CHEFS_SPECIAL_HEADER = "XXX";
        public const string MENU_REGULAR_DISHES_HEADER = "XXX";
        public const string MENU_CURRY_DISHES_HEADER = "XXX";
        public const string MENU_VEGETARIAN_DISHES_HEADER = "XXX";
        public const string MENU_SALADS_HEADER = "XXX";
        public const string MENU_DEVIL_DISHES_HEADER = "XXX";
        public const string MENU_CHICKEN_SPECIAL_HEADER = "XXX";
        public const string MENU_RICE_DISHES_HEADER = "XXX";
        public const string MENU_SWEET_HEADER = "XXX";

        #endregion


        #region - Summary -


        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_SUMMARY = "XXX";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_SUMMARY = "XXX";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_SUMMARY = "XXX";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_SUMMARY = "XXX";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_SUMMARY = "XXX";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_SUMMARY = "XXX";

        public const string MENU_CHEFS_SPECIAL_SUMMARY = "XXX";
        public const string MENU_REGULAR_DISHES_SUMMARY = "XXX";
        public const string MENU_CURRY_DISHES_SUMMARY = "XXX";
        public const string MENU_VEGETARIAN_DISHES_SUMMARY = "XXX";
        public const string MENU_SALADS_SUMMARY = "XXX";
        public const string MENU_DEVIL_DISHES_SUMMARY = "XXX";
        public const string MENU_CHICKEN_SPECIAL_SUMMARY = "XXX";
        public const string MENU_RICE_DISHES_SUMMARY = "XXX";
        public const string MENU_SWEET_SUMMARY = "XXX";

        #endregion


        #region - Template Definition -

        /*

        // HOME ---------------------------------------------------------------------------//

        public const string HOME_INDEX_TEMPLATE = "XXX";

        // ABOUT --------------------------------------------------------------------------//

        public const string HOME_ABOUT_TEMPLATE = "XXX";

        // GALLERY ------------------------------------------------------------------------//

        public const string HOME_GALLERY_TEMPLATE = "XXX";

        // CONTACT ------------------------------------------------------------------------//

        public const string HOME_CONTACT_TEMPLATE = "XXX";

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public const string HOME_PAGENOTFOUND_TEMPLATE = "XXX";

        // MENU ---------------------------------------------------------------------------//

        public const string MENU_INDEX_TEMPLATE = "XXX";

        public const string MENU_CHEFS_SPECIAL_TEMPLATE = "XXX";
        public const string MENU_REGULAR_DISHES_TEMPLATE = "XXX";
        public const string MENU_CURRY_DISHES_TEMPLATE = "XXX";
        public const string MENU_VEGETARIAN_DISHES_TEMPLATE = "XXX";
        public const string MENU_SALADS_TEMPLATE = "XXX";
        public const string MENU_DEVIL_DISHES_TEMPLATE = "XXX";
        public const string MENU_CHICKEN_SPECIAL_TEMPLATE = "XXX";
        public const string MENU_RICE_DISHES_TEMPLATE = "XXX";
        public const string MENU_SWEET_TEMPLATE = "XXX";
        
        */

        #endregion


        #endregion


        #region - Functions -


        #region - Canonical Function -

        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Canonical() { return RootDomain + "/" + HOME_INDEX_CANONICAL; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Canonical() { return RootDomain + "/" + HOME_ABOUT_CANONICAL; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Canonical() { return RootDomain + "/" + HOME_GALLERY_CANONICAL; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Canonical() { return RootDomain + "/" + HOME_CONTACT_CANONICAL; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Canonical() { return RootDomain + "/" + HOME_PAGENOTFOUND_CANONICAL; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Canonical() { return RootDomain + "/" + MENU_INDEX_CANONICAL; }

        public static string Menu_Chefs_Special_Canonical() { return RootDomain + "/" + MENU_CHEFS_SPECIAL_CANONICAL; }
        public static string Menu_Regular_Dishes_Canonical() { return RootDomain + "/" + MENU_REGULAR_DISHES_CANONICAL; }
        public static string Menu_Curry_Dishes_Canonical() { return RootDomain + "/" + MENU_CURRY_DISHES_CANONICAL; }
        public static string Menu_Vegetarian_Dishes_Canonical() { return RootDomain + "/" + MENU_VEGETARIAN_DISHES_CANONICAL; }
        public static string Menu_Salads_Canonical() { return RootDomain + "/" + MENU_SALADS_CANONICAL; }
        public static string Menu_Devil_Dishes_Canonical() { return RootDomain + "/" + MENU_DEVIL_DISHES_CANONICAL; }
        public static string Menu_Chicken_Special_Canonical() { return RootDomain + "/" + MENU_CHICKEN_SPECIAL_CANONICAL; }
        public static string Menu_Rice_Dishes_Canonical() { return RootDomain + "/" + MENU_RICE_DISHES_CANONICAL; }
        public static string Menu_Sweets_Canonical() { return RootDomain + "/" + MENU_SWEET_CANONICAL; }

        #endregion


        #region - Url Function -

        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Url() { return RootDomain + "/" + HOME_INDEX_URL; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Url() { return RootDomain + "/" + HOME_ABOUT_URL; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Url() { return RootDomain + "/" + HOME_GALLERY_URL; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Url() { return RootDomain + "/" + HOME_CONTACT_URL; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Url() { return RootDomain + "/" + HOME_PAGENOTFOUND_URL; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Url() { return RootDomain + "/" + MENU_INDEX_URL; }

        public static string Menu_Chefs_Special_Url() { return RootDomain + "/" + MENU_CHEFS_SPECIAL_URL; }
        public static string Menu_Regular_Dishes_Url() { return RootDomain + "/" + MENU_REGULAR_DISHES_URL; }
        public static string Menu_Curry_Dishes_Url() { return RootDomain + "/" + MENU_CURRY_DISHES_URL; }
        public static string Menu_Vegetarian_Dishes_Url() { return RootDomain + "/" + MENU_VEGETARIAN_DISHES_URL; }
        public static string Menu_Salads_Url() { return RootDomain + "/" + MENU_SALADS_URL; }
        public static string Menu_Devil_Dishes_Url() { return RootDomain + "/" + MENU_DEVIL_DISHES_URL; }
        public static string Menu_Chicken_Special_Url() { return RootDomain + "/" + MENU_CHICKEN_SPECIAL_URL; }
        public static string Menu_Rice_Dishes_Url() { return RootDomain + "/" + MENU_RICE_DISHES_URL; }
        public static string Menu_Sweets_Url() { return RootDomain + "/" + MENU_SWEET_URL; }

        #endregion


        #region - Title Function -

        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Title() { return HOME_INDEX_TITLE; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Title() { return HOME_ABOUT_TITLE; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Title() { return HOME_GALLERY_TITLE; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Title() { return HOME_CONTACT_TITLE; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Title() { return HOME_PAGENOTFOUND_TITLE; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Title() { return MENU_INDEX_TITLE; }

        public static string Menu_Chefs_Special_Title() { return MENU_CHEFS_SPECIAL_TITLE; }
        public static string Menu_Regular_Dishes_Title() { return MENU_REGULAR_DISHES_TITLE; }
        public static string Menu_Curry_Dishes_Title() { return MENU_CURRY_DISHES_TITLE; }
        public static string Menu_Vegetarian_Dishes_Title() { return MENU_VEGETARIAN_DISHES_TITLE; }
        public static string Menu_Salads_Title() { return MENU_SALADS_TITLE; }
        public static string Menu_Devil_Dishes_Title() { return MENU_DEVIL_DISHES_TITLE; }
        public static string Menu_Chicken_Special_Title() { return MENU_CHICKEN_SPECIAL_TITLE; }
        public static string Menu_Rice_Dishes_Title() { return MENU_RICE_DISHES_TITLE; }
        public static string Menu_Sweets_Title() { return MENU_SWEET_TITLE; }

        #endregion


        #region - Description Function -

        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Description() { return HOME_INDEX_DESCRIPTION; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Description() { return HOME_ABOUT_DESCRIPTION; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Description() { return HOME_GALLERY_DESCRIPTION; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Description() { return HOME_CONTACT_DESCRIPTION; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Description() { return HOME_PAGENOTFOUND_DESCRIPTION; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Description() { return MENU_INDEX_DESCRIPTION; }

        public static string Menu_Chefs_Special_Description() { return MENU_CHEFS_SPECIAL_DESCRIPTION; }
        public static string Menu_Regular_Dishes_Description() { return MENU_REGULAR_DISHES_DESCRIPTION; }
        public static string Menu_Curry_Dishes_Description() { return MENU_CURRY_DISHES_DESCRIPTION; }
        public static string Menu_Vegetarian_Dishes_Description() { return MENU_VEGETARIAN_DISHES_DESCRIPTION; }
        public static string Menu_Salads_Description() { return MENU_SALADS_DESCRIPTION; }
        public static string Menu_Devil_Dishes_Description() { return MENU_DEVIL_DISHES_DESCRIPTION; }
        public static string Menu_Chicken_Special_Description() { return MENU_CHICKEN_SPECIAL_DESCRIPTION; }
        public static string Menu_Rice_Dishes_Description() { return MENU_RICE_DISHES_DESCRIPTION; }
        public static string Menu_Sweets_Description() { return MENU_SWEET_DESCRIPTION; }

        #endregion


        #region - Header Function -

        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Header() { return HOME_INDEX_HEADER; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Header() { return HOME_ABOUT_HEADER; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Header() { return HOME_GALLERY_HEADER; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Header() { return HOME_CONTACT_HEADER; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Header() { return HOME_PAGENOTFOUND_HEADER; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Header() { return MENU_INDEX_HEADER; }

        public static string Menu_Chefs_Special_Header() { return MENU_CHEFS_SPECIAL_HEADER; }
        public static string Menu_Regular_Dishes_Header() { return MENU_REGULAR_DISHES_HEADER; }
        public static string Menu_Curry_Dishes_Header() { return MENU_CURRY_DISHES_HEADER; }
        public static string Menu_Vegetarian_Dishes_Header() { return MENU_VEGETARIAN_DISHES_HEADER; }
        public static string Menu_Salads_Header() { return MENU_SALADS_HEADER; }
        public static string Menu_Devil_Dishes_Header() { return MENU_DEVIL_DISHES_HEADER; }
        public static string Menu_Chicken_Special_Header() { return MENU_CHICKEN_SPECIAL_HEADER; }
        public static string Menu_Rice_Dishes_Header() { return MENU_RICE_DISHES_HEADER; }
        public static string Menu_Sweets_Header() { return MENU_SWEET_HEADER; }

        #endregion


        #region - Summary Function -

        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Summary() { return HOME_INDEX_SUMMARY; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Summary() { return HOME_ABOUT_SUMMARY; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Summary() { return HOME_GALLERY_SUMMARY; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Summary() { return HOME_CONTACT_SUMMARY; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Summary() { return HOME_PAGENOTFOUND_SUMMARY; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Summary() { return MENU_INDEX_SUMMARY; }

        public static string Menu_Chefs_Special_Summary() { return MENU_CHEFS_SPECIAL_SUMMARY; }
        public static string Menu_Regular_Dishes_Summary() { return MENU_REGULAR_DISHES_SUMMARY; }
        public static string Menu_Curry_Dishes_Summary() { return MENU_CURRY_DISHES_SUMMARY; }
        public static string Menu_Vegetarian_Dishes_Summary() { return MENU_VEGETARIAN_DISHES_SUMMARY; }
        public static string Menu_Salads_Summary() { return MENU_SALADS_SUMMARY; }
        public static string Menu_Devil_Dishes_Summary() { return MENU_DEVIL_DISHES_SUMMARY; }
        public static string Menu_Chicken_Special_Summary() { return MENU_CHICKEN_SPECIAL_SUMMARY; }
        public static string Menu_Rice_Dishes_Summary() { return MENU_RICE_DISHES_SUMMARY; }
        public static string Menu_Sweets_Summary() { return MENU_SWEET_SUMMARY; }

        #endregion


        #region - Keywords Function -


        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Keywords() { return HOME_INDEX_KEYWORDS; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Keywords() { return HOME_ABOUT_KEYWORDS; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Keywords() { return HOME_GALLERY_KEYWORDS; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Keywords() { return HOME_CONTACT_KEYWORDS; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Keywords() { return HOME_PAGENOTFOUND_KEYWORDS; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Keywords() { return MENU_INDEX_KEYWORDS; }

        public static string Menu_Chefs_Special_Keywords() { return MENU_CHEFS_SPECIAL_KEYWORDS; }
        public static string Menu_Regular_Dishes_Keywords() { return MENU_REGULAR_DISHES_KEYWORDS; }
        public static string Menu_Curry_Dishes_Keywords() { return MENU_CURRY_DISHES_KEYWORDS; }
        public static string Menu_Vegetarian_Dishes_Keywords() { return MENU_VEGETARIAN_DISHES_KEYWORDS; }
        public static string Menu_Salads_Keywords() { return MENU_SALADS_KEYWORDS; }
        public static string Menu_Devil_Dishes_Keywords() { return MENU_DEVIL_DISHES_KEYWORDS; }
        public static string Menu_Chicken_Special_Keywords() { return MENU_CHICKEN_SPECIAL_KEYWORDS; }
        public static string Menu_Rice_Dishes_Keywords() { return MENU_RICE_DISHES_KEYWORDS; }
        public static string Menu_Sweets_Keywords() { return MENU_SWEET_KEYWORDS; }

        #endregion


        #region - Template Function -

        /*
        // HOME ---------------------------------------------------------------------------//

        public static string Home_Index_Template() { return HOME_INDEX_TEMPLATE; }

        // ABOUT --------------------------------------------------------------------------//

        public static string Home_About_Template() { return HOME_ABOUT_TEMPLATE; }

        // GALLERY ------------------------------------------------------------------------//

        public static string Home_Gallery_Template() { return HOME_GALLERY_TEMPLATE; }

        // CONTACT ------------------------------------------------------------------------//

        public static string Home_Contact_Template() { return HOME_CONTACT_TEMPLATE; }

        // PAGE NOT FOUND -----------------------------------------------------------------//

        public static string Home_PageNotFound_Template() { return HOME_PAGENOTFOUND_TEMPLATE; }

        // MENU ---------------------------------------------------------------------------//

        public static string Menu_Index_Template() { return MENU_INDEX_TEMPLATE; }

        public static string Menu_Chefs_Special_Template() { return MENU_CHEFS_SPECIAL_TEMPLATE; }
        public static string Menu_Regular_Dishes_Template() { return MENU_REGULAR_DISHES_TEMPLATE; }
        public static string Menu_Curry_Dishes_Template() { return MENU_CURRY_DISHES_TEMPLATE; }
        public static string Menu_Vegetarian_Dishes_Template() { return MENU_VEGETARIAN_DISHES_TEMPLATE; }
        public static string Menu_Salads_Template() { return MENU_SALADS_TEMPLATE; }
        public static string Menu_Devil_Dishes_Template() { return MENU_DEVIL_DISHES_TEMPLATE; }
        public static string Menu_Chicken_Special_Template() { return MENU_CHICKEN_SPECIAL_TEMPLATE; }
        public static string Menu_Rice_Dishes_Template() { return MENU_RICE_DISHES_TEMPLATE; }
        public static string Menu_Sweets_Template() { return MENU_SWEET_TEMPLATE; }
        */

        #endregion


        #endregion


        #region - Sitemap Generation -

        public static bool Generate_Sitemap(string stringFilePath)
        {
            #region - Initialise -

            List<string> listStringCanonicalURL = new List<string>();

            // HOME ---------------------------------------------------------------------------//

            listStringCanonicalURL.Add(Home_Index_Canonical());

            // ABOUT --------------------------------------------------------------------------//

            listStringCanonicalURL.Add(Home_About_Canonical());

            // GALLERY ------------------------------------------------------------------------//

            listStringCanonicalURL.Add(Home_Gallery_Canonical());

            // CONTACT ------------------------------------------------------------------------//

            listStringCanonicalURL.Add(Home_Contact_Canonical());

            // MENU ---------------------------------------------------------------------------//

            listStringCanonicalURL.Add(Menu_Index_Canonical());

            #endregion

            string stringDateTimeCurrent = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-ddTHH:mm:ss+00:00");

            XNamespace xNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9";
            XNamespace xNamespaceImage = "http://www.google.com/schemas/sitemap-image/1.1";

            XDocument xDocument = new XDocument(new XDeclaration("1.0", "UTF-8", "yes"),
                new XElement(xNamespace + "urlset", new XAttribute(XNamespace.Xmlns + "image", xNamespaceImage),

                    /*
                    new XElement(xNamespace + "url",
                        new XElement(xNamespace + "loc", Home_Index_Canonical()),
                        new XElement(xNamespace + "lastmod", stringDateTimeCurrent),
                        new XElement(xNamespace + "changefreq", "hourly"),
                        new XElement(xNamespace + "priority", "1.0"),
                        new XElement(xNamespaceImage + "image",
                            new XElement(xNamespaceImage + "loc", "http://www.caddrawings.com.au/CAD-DRAWINGS.png"),
                            new XElement(xNamespaceImage + "title", HOME_INDEX_TITLE)
                        ),
                        new XElement(xNamespaceImage + "image",
                            new XElement(xNamespaceImage + "loc", "http://www.caddrawings.com.au/CAD-DRAWINGS-Logo.png"),
                            new XElement(xNamespaceImage + "title", HOME_INDEX_TITLE)
                        )
                    ),*/

                    from string stringCanonicalURL in listStringCanonicalURL
                    select
                    new XElement(xNamespace + "url",
                        new XElement(xNamespace + "loc", stringCanonicalURL),
                        new XElement(xNamespace + "lastmod", stringDateTimeCurrent),
                        new XElement(xNamespace + "changefreq", "daily"),
                        new XElement(xNamespace + "priority", "1.0")
                    )
                )
            );

            xDocument.Save(stringFilePath);

            return true;
        }

        #endregion

    }
}