using Employees_CosmosDB.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Employees_CosmosDB.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly string CosmosDBAccountURI = "https://localhost:8081/";
        private readonly string CosmosDBAccountPrimaryKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private readonly string CosmosDbName = "EmployeeManagementDB";
        private readonly string CosmosDbContainerName = "Employees";

        private Container ContainerClient()
        {
            CosmosClient cosmosDbClient = new CosmosClient(CosmosDBAccountURI, CosmosDBAccountPrimaryKey);
            Container containerClient = cosmosDbClient.GetContainer(CosmosDbName, CosmosDbContainerName);
            return containerClient;
        }

        //Get All Employees
        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetails()
        {
            try
            {
                var container = ContainerClient();
                var sqlQuery = "SELECT * FROM c";
                QueryDefinition queryDefinition = new QueryDefinition(sqlQuery);
                FeedIterator<EmployeeModel> queryResultSetIterator = container.GetItemQueryIterator<EmployeeModel>(queryDefinition);
                List<EmployeeModel> employees = new List<EmployeeModel>();
                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<EmployeeModel> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (EmployeeModel employee in currentResultSet)
                    {
                        employees.Add(employee);
                    }
                }
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Method Add Employees
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeModel employee)
        {
            try
            {
                var container = ContainerClient();
                var response = await container.CreateItemAsync(employee, new PartitionKey(employee.Department));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Method Get Employees By Id
        [HttpGet]
        public async Task<IActionResult> GetEmployeeDetailsById(string employeeId, string partitionKey)
        {
            try
            {
                var container = ContainerClient();
                ItemResponse<EmployeeModel> response = await container.ReadItemAsync<EmployeeModel>(employeeId, new PartitionKey(partitionKey));
                return Ok(response.Resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //Update Employee
        [HttpPut]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel emp, string partitionKey)
        {
            try
            {
                var container = ContainerClient();
                ItemResponse<EmployeeModel> res = await container.ReadItemAsync<EmployeeModel>(emp.id, new PartitionKey(partitionKey));
                //Get Existing Item
                var existingItem = res.Resource;
                //Replace existing item values with new values
                existingItem.Name = emp.Name;
                existingItem.Country = emp.Country;
                existingItem.City = emp.City;
                existingItem.Department = emp.Department;
                existingItem.Designation = emp.Designation;
                var updateRes = await container.ReplaceItemAsync(existingItem, emp.id, new PartitionKey(partitionKey));
                return Ok(updateRes.Resource);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //Delete Employee
        [HttpDelete]
        public async Task<IActionResult> DeleteEmployee(string empId, string partitionKey)
        {
            try
            {
                var container = ContainerClient();
                var response = await container.DeleteItemAsync<EmployeeModel>(empId, new PartitionKey(partitionKey));
                return Ok(response.StatusCode);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
