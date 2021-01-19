using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Dapper;

namespace DapperExercise
{
    class Program
    {
        internal static IDbConnection Connection = new SqlConnection("Data Source=.;Initial Catalog=GETS_SCHOOL;Integrated Security=True;MultipleActiveResultSets=True");

        static void Main(string[] args)
        {
            try
            {
                var person = new Person
                {
                    UserName = "jack",
                    Email = "jewf@163.com",
                    Address = "深圳"
                };
                // Insert(null);    //插入一个
                // Inserts(null);   //多行插入
                // Select("jack");   //根据UserName进行查询
                // Update(person);  //更新一个
                // Delete(2);   //删除某一个
                #region in查询多个
                /* var ids = new int[] { 1, 3 };   
                   var temp =  QueryIn(ids); 
                   foreach (var item in temp)
                   {
                       Console.WriteLine("UserName:{0}",item.UserName);
                       Console.WriteLine("Email:{0}",item.Email);
                       Console.WriteLine("Address:{0}",item.Address);
                       Console.WriteLine("====================");
                       Console.WriteLine();
                   }
                */
                #endregion

                  QuerySqls();  //多条sql语句一起执行
                //QueryJoin();     //Join多表查询
                //QueryProcedure();   //存储过程查询

            }
            catch
            {
                Console.WriteLine("Fail");
            }
        }

        private static void Insert(Person person)
        {
            var result = Connection.Execute("Insert into Users values (@UserName, @Email, @Address)",
                new { UserName = "jack", Email = "380234234@qq.com", Address = "上海" });
        }

        private static void Inserts(List<Person> people)
        {
            var usersList = Enumerable.Range(0, 10).Select(i => new Person()
            {
                UserName = i + "jkl",
                Email = i + "qq.com",
                Address = i + "上海"
            });
            var result = Connection.Execute("Insert Into Users values (@UserName, @Email, @Address)", usersList);
        }

        private static void Select(string userName)
        {
            var i = 0;
            var query = Connection.Query<Person>("Select * from Users Where UserName=@UserName", new { UserName = userName });
            foreach (var item in query)
            {
                Console.WriteLine($"============={i}=============");
                Console.WriteLine("UserName:" + item.UserName);
                Console.WriteLine("Email:" + item.Email);
                Console.WriteLine("Address:" + item.Address);
                i++;
                Console.WriteLine();
            }
        }

        private static void Update(Person person)
        {
            Connection.Execute("Update Users set UserName = @UserName, Email = @Email, Address = @Address where UserID = @UserID", person);
        }

        private static void Delete(int userId)
        {
            Connection.Execute("Delete from Users where UserID = @UserID", new { UserID = userId });
        }

        private static List<Person> QueryIn(int[] ids)
        {
            const string sql = "select * from Users where UserID in @ids";
            return Connection.Query<Person>(sql, new { ids }).ToList();
        }

        private static void QuerySqls()
        {
            //sql查询语句跟下面Read读的顺序有关
            const string sql = "select * from Product; select * from Users; ";
            var multiReader = Connection.QueryMultiple(sql);
            var productList = multiReader.Read<Product>();
            var userList = multiReader.Read<Person>();
            foreach (var product in productList.ToList())
            {
                Console.WriteLine(product.ProductName);
            }

            foreach (var person in userList.ToList())
            {
                Console.WriteLine(person.UserName);
            }
            multiReader.Dispose();

        }

        private static void QueryJoin()
        {
            const string sql = @"select p.ProductName, p.CreateTime, u.UserID, u.UserName
                                from Product as p
                                join Users as u
                                on p.UserID = u.UserID;";
            var result = Connection.Query(sql).ToList();
            foreach (var item in result)
            {
                Console.WriteLine(item.ProductName);
                Console.WriteLine(item.CreateTime);
                Console.WriteLine(item.UserID);
                Console.WriteLine(item.UserName);
                Console.WriteLine("==================");
                Console.WriteLine();
            }
        }

        private static void QueryProcedure()
        {
            var info = Connection.Query<Person>("sp_GetUsers", new { id = 5 },
                commandType: CommandType.StoredProcedure);
        }
    }
}