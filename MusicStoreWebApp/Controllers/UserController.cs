using Microsoft.AspNetCore.Mvc;
using MusicStoreWebApp.Models;
using System.Data.SqlClient;
namespace MusicStoreWebApp.Controllers
{

    public class UserController: Controller {
        private string conn_string = "Server=SHREYAS\\SQLEXPRESS;Database=MusicStore;Integrated Security=true;";

        private SqlConnection GetOpenConnection() {
            var conn = new SqlConnection(conn_string);
            conn.Open();
            return conn;
        }

        public IActionResult viewCustomer() {
            using (var conn = GetOpenConnection()) {
                var cmd = new SqlCommand("select * from Customers", conn);
                var cReader = cmd.ExecuteReader();
                IList<CustomerViewModel> cust_model_list = new List<CustomerViewModel>();
                while (cReader.Read()) {
                    CustomerViewModel model = new CustomerViewModel {
                        cust_id = Convert.ToInt64(cReader[0]),
                        cust_email = Convert.ToString(cReader[1]),
                        cust_name = Convert.ToString(cReader[2]),
                        cust_phone = Convert.ToString(cReader[3]),
                        cust_shipping_addr = Convert.ToString(cReader[4]),
                        preferred_store_id = Convert.ToInt64(cReader[5]),
                        cust_username = Convert.ToString(cReader[6]),
                        cust_billing_addr = Convert.ToString(cReader[7]),

                    };
                    cust_model_list.Add(model);
                }
                return View(cust_model_list);
            }
        }

        [HttpPost]
        public IActionResult Save(IFormCollection collection) {

            //var userType = collection["user_type"];
            //if (userType == "customer") {
              //  SaveCustomer(collection);
            //} //else if (userType == "employee") {
              //  SaveEmployee(collection);
            //}

            

            return RedirectToAction("Index", "Home");
        }

        public IActionResult SaveCustomer(CustomerViewModel cmodel) {
            var phone = Convert.ToString(cmodel.cust_phone);
            if (!phone.StartsWith("+1")) {
                phone = "+1 " + phone;
            }
            CustomerViewModel model = new CustomerViewModel {
                cust_id = Convert.ToInt64(cmodel.cust_id),

                cust_email = cmodel.cust_email,
                cust_name = cmodel.cust_name,
                cust_phone = phone,
                cust_shipping_addr = cmodel.cust_shipping_addr,
                preferred_store_id = Convert.ToInt64(cmodel.preferred_store_id),
                cust_username = cmodel.cust_username,
                cust_billing_addr = cmodel.cust_billing_addr,
            };

            using (var conn = GetOpenConnection()) {
                string query = "INSERT INTO Customers (cust_id, cust_email, cust_name, cust_phone, cust_shipping_addr, preferred_store_id, cust_username, cust_billing_addr)";
                query += " VALUES (@cust_id, @cust_email, @cust_name, @cust_phone, @cust_shipping_addr, @preferred_store_id, @cust_username, @cust_billing_addr)";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@cust_id", model.cust_id);
                cmd.Parameters.AddWithValue("@cust_email", model.cust_email);
                cmd.Parameters.AddWithValue("@cust_name", model.cust_name);
                cmd.Parameters.AddWithValue("@cust_phone", model.cust_phone);
                cmd.Parameters.AddWithValue("@cust_shipping_addr", model.cust_shipping_addr);
                cmd.Parameters.AddWithValue("@preferred_store_id", model.preferred_store_id);
                cmd.Parameters.AddWithValue("@cust_username", model.cust_username);
                cmd.Parameters.AddWithValue("@cust_billing_addr", model.cust_billing_addr);
                cmd.ExecuteNonQuery();
            }

            return View(model);
        }


        private void SaveCustomer(IFormCollection collection) {
            var phone = Convert.ToString(collection["cust_phone"]);
            if (!phone.StartsWith("+1")) {
                phone = "+1 " + phone;
            }
            CustomerViewModel model = new CustomerViewModel {
                cust_id = Convert.ToInt64(collection["cust_id"]),

                cust_email = collection["cust_email"],
                cust_name = collection["cust_name"],
                cust_phone = phone,
                cust_shipping_addr = collection["cust_shipping_addr"],
                preferred_store_id = Convert.ToInt64(collection["preferred_store_id"]),
                cust_username = collection["cust_username"],
                cust_billing_addr = collection["cust_billing_addr"],
            };

            using (var conn = GetOpenConnection()) {
                string query = "INSERT INTO Customers (cust_id, cust_email, cust_name, cust_phone, cust_shipping_addr, preferred_store_id, cust_username, cust_billing_addr)";
                query += " VALUES (@cust_id, @cust_email, @cust_name, @cust_phone, @cust_shipping_addr, @preferred_store_id, @cust_username, @cust_billing_addr)";
                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@cust_id", model.cust_id);
                cmd.Parameters.AddWithValue("@cust_email", model.cust_email);
                cmd.Parameters.AddWithValue("@cust_name", model.cust_name);
                cmd.Parameters.AddWithValue("@cust_phone", model.cust_phone);
                cmd.Parameters.AddWithValue("@cust_shipping_addr", model.cust_shipping_addr);
                cmd.Parameters.AddWithValue("@preferred_store_id", model.preferred_store_id);
                cmd.Parameters.AddWithValue("@cust_username", model.cust_username);
                cmd.Parameters.AddWithValue("@cust_billing_addr", model.cust_billing_addr);
                cmd.ExecuteNonQuery();
            }
        }

        private void SaveEmployee(IFormCollection collection) {
            var phone = Convert.ToString(collection["emp_phone"]);
            if (!phone.StartsWith("+1")) {
                phone = "+1 " + phone;
            }

            EmployeeViewModel model = new EmployeeViewModel {
                emp_id = Convert.ToInt64(collection["emp_id"]),
                emp_email = collection["emp_email"],
                emp_name = collection["emp_name"],
                emp_phone = phone,
                emp_addr = collection["emp_addr"],
                emp_store_id = Convert.ToInt64(collection["emp_store_id"]),
            };
            using (var conn = GetOpenConnection()) {
                var query = "INSERT INTO Employees (emp_id, emp_name, emp_email, emp_phone, emp_addr, user_type, emp_store_id) " +
                           "VALUES (@emp_id, @emp_name, @emp_email, @emp_phone, @emp_addr, @user_type, @user_type)";

                var cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@emp_id", model.emp_id);
                cmd.Parameters.AddWithValue("@emp_name", model.emp_name);
                cmd.Parameters.AddWithValue("@emp_email", model.emp_email);
                
                cmd.Parameters.AddWithValue("@emp_phone", model.emp_phone);
                cmd.Parameters.AddWithValue("@emp_addr", model.emp_addr);
                cmd.Parameters.AddWithValue("@user_type", model.user_type);
                cmd.Parameters.AddWithValue("@emp_store_id", model.emp_store_id);
                cmd.ExecuteNonQuery();
            }
        }
        
        public IActionResult Add() {
            return View();
        }
    }
}
