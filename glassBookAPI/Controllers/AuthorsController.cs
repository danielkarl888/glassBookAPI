﻿using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace glassBookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        // GET authors
        [HttpGet]
        public IEnumerable<Author> Get()
        {
            List<Author> authors = new List<Author>();
            try
            {
                string connectionString = "server=localhost;database=glassBook;uid=root;pwd=Karl5965;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "SELECT * FROM author limit 500";
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int author_id = reader.GetInt32(0);
                                string author_name = reader.GetString(1);

                                Author author = new Author();
                                author.Author_id = author_id;
                                author.Author_name = author_name;
                                authors.Add(author);
                                // retrieve data for other columns as needed

                            }
                        }
                    }
                    connection.Close();
                }
                return authors;
            }
            catch (Exception ex)
            {
                Author author = new Author();
                author.Author_name = "error with the server";
                authors.Add(author);
                return authors;
            }
        }


    }
}
