using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using ServiceStack;
using Newtonsoft.Json;

namespace StackPOS
{
    //Request DTO
    public class Hello
    {
        public string Name { get; set; }
    }

    //Response DTO
    public class HelloResponse
    {
        public string Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; } //Where Exceptions get auto-serialized
    }

    //Can be called via any endpoint or format, see: http://mono.servicestack.net/ServiceStack.Hello/
    public class HelloService : Service
    {
        public object Any(Hello request)
        {
            return new HelloResponse { Result = "Hello, " + request.Name };
        }
    }

    ////REST Resource DTO
    //[Route("/todos")]
    //[Route("/todos/{Ids}")]
    //public class Todos : IReturn<List<Todo>>
    //{
    //    public long[] Ids { get; set; }
    //    public Todos(params long[] ids)
    //    {
    //        this.Ids = ids;
    //    }
    //}

    //[Route("/todos", "POST")]
    //[Route("/todos/{Id}", "PUT")]
    //public class Todo : IReturn<Todo>
    //{
    //    public long Id { get; set; }
    //    public string Content { get; set; }
    //    public int Order { get; set; }
    //    public bool Done { get; set; }
    //}

    //public class TodosService : Service
    //{
    //    public TodoRepository Repository { get; set; }  //Injected by IOC

    //    public object Get(Todos request)
    //    {
    //        return request.Ids.IsEmpty()
    //            ? Repository.GetAll()
    //            : Repository.GetByIds(request.Ids);
    //    }

    //    public object Post(Todo todo)
    //    {
    //        return Repository.Store(todo);
    //    }

    //    public object Put(Todo todo)
    //    {
    //        return Repository.Store(todo);
    //    }

    //    public void Delete(Todos request)
    //    {
    //        Repository.DeleteByIds(request.Ids);
    //    }
    //}
    
    //public class TodoRepository
    //{
    //    List<Todo> todos = new List<Todo>();
        
    //    public List<Todo> GetByIds(long[] ids)
    //    {
    //        return todos.Where(x => ids.Contains(x.Id)).ToList();
    //    }

    //    public List<Todo> GetAll()
    //    {
    //        return todos;
    //    }

    //    public Todo Store(Todo todo)
    //    {
    //        var existing = todos.FirstOrDefault(x => x.Id == todo.Id);
    //        if (existing == null)
    //        {
    //            var newId = todos.Count > 0 ? todos.Max(x => x.Id) + 1 : 1;
    //            todo.Id = newId;
    //            todos.Add(todo);
    //        }
    //        else
    //        {
    //            existing.PopulateWith(todo);
    //        }
    //        return todo;
    //    }

    //    public void DeleteByIds(params long[] ids)
    //    {
    //        todos.RemoveAll(x => ids.Contains(x.Id));
    //    }
    //}


/*  Example calling above Service with ServiceStack's C# clients:

    var client = new JsonServiceClient(BaseUri);
    List<Todo> all = client.Get(new Todos());           // Count = 0

    var todo = client.Post(
        new Todo { Content = "New TODO", Order = 1 });      // todo.Id = 1
    all = client.Get(new Todos());                      // Count = 1

    todo.Content = "Updated TODO";
    todo = client.Put(todo);                            // todo.Content = Updated TODO

    client.Delete(new Todos(todo.Id));
    all = client.Get(new Todos());                      // Count = 0

*/


    //REST Resource DTO
    [Route("/products")]
    [Route("/products/{Ids}")]
    public class Products : IReturn<List<Product>>
    {
        public long[] Ids { get; set; }
        public Products(params long[] ids)
        {
            this.Ids = ids;
        }
    }

    [Route("/products", "POST")]
    [Route("/products/{Id}", "PUT")]
    public class Product : IReturn<Product>
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int OnHand { get; set; }
        public string Category { get; set; }
    }

    [Route("/transactions")]
    [Route("/transactions/{Ids}")]
    public class Transactions : IReturn<List<Transaction>>
    {
        public long[] Ids { get; set; }
        public Transactions(params long[] ids)
        {
            this.Ids = ids;
        }
    }

    [Route("/transactions", "POST")]
    [Route("/transactions/{Id}", "PUT")]
    public class Transaction : IReturn<Transaction>
    {
        public long Id { get; set; }
        public DateTime TransactionDate{ get; set; }
        public decimal TotalAmount{ get; set; }
        public int Lines { get; set; }
        public string Comments { get; set; }
    }

    public class ProductService : Service
    {
        public ProductRepository Repository { get; set; }  //Injected by IOC

        public object Get(Products request)
        {
            return request.Ids.IsEmpty()
                ? Repository.GetAll()
                : Repository.GetByIds(request.Ids);
        }

        public object Post(Product todo)
        {
            return Repository.Store(todo);
        }

        public object Put(Product todo)
        {
            return Repository.Store(todo);
        }

        public void Delete(Products request)
        {
            Repository.DeleteByIds(request.Ids);
        }
    }

    public class TransactionService : Service
    {
        public TransactionRepository Repository { get; set; }  //Injected by IOC

        public object Get(Transactions transactions)
        {
            return transactions.Ids.IsEmpty()
                ? Repository.GetAll()
                : Repository.GetByIds(transactions.Ids);
        }

        public object Post(Transaction transaction)
        {
            return Repository.Store(transaction);
        }

        public object Put(Transaction transaction)
        {
            return Repository.Store(transaction);
        }

        public void Delete(Transactions transactions)
        {
            Repository.DeleteByIds(transactions.Ids);
        }
    }


    public class ProductRepository
    {
        List<Product> _products = GetJsonObjects();

        public ProductRepository(): base()
        {
            // populate the products by reading the json object file
            _products = GetJsonObjects();

        }
        public static List<Product> GetJsonObjects()
        {
            List<Product> items = new List<Product>();

            using (var w = new System.Net.WebClient())
            {
                var json_data = string.Empty;
                // attempt to download JSON data as a string
                try
                {
                    json_data = w.DownloadString("http://vspos.azurewebsites.net/app/products.json");
                    items = JsonConvert.DeserializeObject<List<Product>>(json_data);
                }
                catch (Exception ex)
                {

                }
            }
            return items;
        }

        public List<Product> GetByIds(long[] ids)
        {
            return _products.Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public Product Store(Product todo)
        {
            var existing = _products.FirstOrDefault(x => x.Id == todo.Id);
            if (existing == null)
            {
                var newId = _products.Count > 0 ? _products.Max(x => x.Id) + 1 : 1;
                todo.Id = newId;
                _products.Add(todo);
            }
            else
            {
                existing.PopulateWith(todo);
            }
            return todo;
        }

        public void DeleteByIds(params long[] ids)
        {
            _products.RemoveAll(x => ids.Contains(x.Id));
        }
    }

    public class TransactionRepository
    {
        List<Transaction> _transactions = new List<Transaction>();

        public TransactionRepository()
            : base()
        {
            // populate the products by reading the json object file
            _transactions = GetMockData();

        }
        public static List<Transaction> GetMockData()
        {
            List<Transaction> items = new List<Transaction>();

            Transaction tran = new Transaction();
            tran.Id = 1;
            tran.Lines = 2;
            tran.TotalAmount = 25.50m;
            tran.Comments = "Good Customer. Must recommend Membership next time!";
            tran.TransactionDate = DateTime.Today.AddDays(-10);
            items.Add(tran);

            tran = new Transaction();
            tran.Id = 2;
            tran.Lines = 2;
            tran.TotalAmount = 5.99m;
            tran.Comments = String.Empty;
            tran.TransactionDate = DateTime.Today.AddDays(-10);
            items.Add(tran);

            tran = new Transaction();
            tran.Id = 3;
            tran.Lines = 1;
            tran.TotalAmount = 4.99m;
            tran.Comments = "Less sales today!";
            tran.TransactionDate = DateTime.Today.AddDays(-9);            
            items.Add(tran);

            tran = new Transaction();
            tran.Id = 4;
            tran.Lines = 10;
            tran.TotalAmount = 44.99m;
            tran.Comments = "Awesome sales today!";
            tran.TransactionDate = DateTime.Today.AddDays(-8);
            items.Add(tran);

            return items;
        }

        public List<Transaction> GetByIds(long[] ids)
        {
            return _transactions.Where(x => ids.Contains(x.Id)).ToList();
        }

        public List<Transaction> GetAll()
        {
            return _transactions;
        }

        public Transaction Store(Transaction transaction)
        {
            var existing = _transactions.FirstOrDefault(x => x.Id == transaction.Id);
            if (existing == null)
            {
                var newId = _transactions.Count > 0 ? _transactions.Max(x => x.Id) + 1 : 1;
                transaction.Id = newId;
                _transactions.Add(transaction);
            }
            else
            {
                existing.PopulateWith(transaction);
            }
            return transaction;
        }

        public void DeleteByIds(params long[] ids)
        {
            _transactions.RemoveAll(x => ids.Contains(x.Id));
        }
    }
}
