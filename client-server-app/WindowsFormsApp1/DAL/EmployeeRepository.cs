using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace WindowsFormsApp1.DAL
{


    public class EmployeeRepository : BaseRepository
    {

        public EmployeeRepository(string login, string pass) : base(login, pass) { }


        // E X P O R T


        public async Task<int> AddEmployee(Employee employee)
        {
            int res = 0;
            var p = new DynamicParameters();
            p.Add("@LastName", employee.LastName);
            p.Add("@FirstName", employee.FirstName);
            p.Add("@HiringDate", employee.EmploymentContract.First().HiringDate);
            p.Add("@JobTitleID", employee.EmploymentContract.First().JobTitleID);
            p.Add("@Email", employee.Email.First().Value);
            p.Add("@CellPhone", employee.CellPhone.First().Value);
            p.Add("@BirthDate", employee.BirthDate);
            p.Add("@Gender", employee.Gender);
            p.Add("@Passport", employee.Passport);
            p.Add("@MaritalStatusID", employee.MaritalStatusID);
            p.Add("@City", employee.Address.First().City);
            p.Add("@Street", employee.Address.First().Street);
            p.Add("@Building", employee.Address.First().Building);
            p.Add("@Appartment", employee.Address.First().Appartment);
            p.Add("@EmergencyNumber", employee.EmergencyContact.First().CellPhone);
            p.Add("@EmergencyName", employee.EmergencyContact.First().FirstName);
            p.Add("@EmergencyLastName", employee.EmergencyContact.First().LastName);
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_AddStaff", p, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> AddChild(Employee employee)
        {
            Child child = employee.Children.Last();
            var p = new DynamicParameters();
            p.Add("@PersonID", employee.PersonID);
            p.Add("@BirthDate", child.BirthDate);
            p.Add("@FirstName", child.FirstName);
            p.Add("@LastName", child.LastName);
            p.Add("@Gender", child.Gender);
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await  c.ExecuteAsync("p_AddChildren", p, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> AddEmerg(Employee employee)
        {
            EmergencyContact contact = employee.EmergencyContact.Last();
            var p = new DynamicParameters();
            p.Add("@PersonID", employee.PersonID);
            p.Add("@LastName", contact.LastName);
            p.Add("@FirstName", contact.FirstName);
            p.Add("@CellPhone", contact.CellPhone);
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_AddEmergencyContact", p, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                     {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> AddAddress(Employee employee)
        {
            Address address = employee.Address.Last();
            var p = new DynamicParameters();
            p.Add("@PersonID", employee.PersonID);
            p.Add("@City", address.City);
            p.Add("@Street", address.Street);
            p.Add("@Building", address.Building);
            p.Add("@Appartment", address.Appartment);
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_AddAddress", p, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> AddJob(Employee employee)
        {
            int id = employee.EmploymentContract.Last().JobTitleID;
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_AddEmploymentContract", new { employee.PersonID, id }, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> AddCellPhone(Employee employee)
        {
            string Cell = employee.CellPhone.Last().Value;
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_AddCellPhone", new { employee.PersonID, Cell }, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> AddEmail(Employee employee)
        {
            string Email = employee.Email.Last().Value;
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_AddEmail", new { employee.PersonID, Email }, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

      
        public async Task<int> RemoveOptionalField(Employee employee, string property, int id)
        {
            string procedure = "p_Remove{0}";
            property = char.ToUpper(property[0]) + property.Substring(1);
            procedure = string.Format(procedure, property);
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync(procedure, new { employee.PersonID, id }, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);          
        }

        public async Task<int> Terminate(Employee employee, int ProfID)
        {
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_Terminate", new { ProfID }, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }


        public async Task<int> OffLeave(Employee employee, int id)
        {
            EmploymentContract con = null;
            foreach (EmploymentContract contract in employee.EmploymentContract)
            {
                if (contract.ProfID == id) con = contract;
            }
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_PersonOffLeave", con, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }

        public async Task<int> OnLeave(Employee employee, int id)
        {
            EmploymentContract con = null;
            foreach (EmploymentContract contract in employee.EmploymentContract)
            {
                if (contract.ProfID == id) con = contract;
            }
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        res = await c.ExecuteAsync("p_PersonOnLeave", con, transaction: t, commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }


        public async Task<int> Update(Employee employee)
        {
            int res = 0;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {                                                                     
                        res += await c.ExecuteAsync("update AllStaff set LastName = @LastName, FirstName = @FirstName, Passport = @Passport, BirthDate = CONVERT(date, @Birthdate, 103), Gender = @Gender, MaritalStatusID = @MaritalStatusID where PersonID=@PersonID", employee, transaction: t).ConfigureAwait(false);
                        foreach (KeyValue cellphone in employee.CellPhone)
                        {
                            res += await c.ExecuteAsync("update CellPhone set CellPhone = @Value where CellID=@ID", cellphone, transaction: t).ConfigureAwait(false);
                        }
                        foreach (KeyValue email in employee.Email)
                        {
                            res += await c.ExecuteAsync("update Email set Email = @Value where EmailID=@ID", email, transaction: t).ConfigureAwait(false);
                        }
                        foreach (Child child in employee.Children)
                        {
                            res += await c.ExecuteAsync("update Children set LastName = @LastName, FirstName = @FirstName, BirthDate = CONVERT(date, @Birthdate, 103), Gender = @Gender where ChildID=@ChildID", child, transaction: t).ConfigureAwait(false);
                        }
                        foreach (EmergencyContact contact in employee.EmergencyContact)
                        {
                            res += await c.ExecuteAsync("update EmergencyContacts set LastName = @LastName, FirstName = @FirstName, CellPhone=@CellPhone where EmergencyContactID=@EmergencyContactID", contact, transaction: t).ConfigureAwait(false);
                        }
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return res;
            }).ConfigureAwait(false);
        }





        // I M P O R T



        public async Task<Employee> FillData(Employee emp)
        {
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        emp.CellPhone = (await c.QueryAsync<KeyValue>("select  CellID as ID, CellPhone as Value from AllStaff join CellPhone on AllStaff.PersonID=CellPhone.PersonID where AllStaff.PersonID = @PersonID", emp, transaction: t).ConfigureAwait(false)).ToList();
                        emp.Email = (await c.QueryAsync<KeyValue>("select  EmailID as ID, Email as Value from AllStaff join Email on AllStaff.PersonID=Email.PersonID where AllStaff.PersonID = @PersonID", emp, transaction: t).ConfigureAwait(false)).ToList();
                        emp.EmergencyContact = (await c.QueryAsync<EmergencyContact>("select c.EmergencyContactID,c.LastName,c.FirstName,c.CellPhone from AllStaff as a join EmergencyContactRelation as b on a.PersonID=b.PersonID join EmergencyContacts as c on b.EmergencyContactID=c.EmergencyContactID where a.PersonID = @PersonID", emp, transaction: t).ConfigureAwait(false)).ToList();
                        emp.Children = (await c.QueryAsync<Child>("select c.ChildID, c.LastName,c.FirstName,CONVERT(VARCHAR, c.BirthDate, 104) as BirthDate,c.Gender from AllStaff join ParentChildRelation as b on b.ParentID=AllStaff.PersonID join Children as c on b.ChildID=c.ChildID where AllStaff.PersonID = @PersonID", emp, transaction: t).ConfigureAwait(false)).ToList();
                        emp.Address = (await c.QueryAsync<Address>("select Country,City,Street,Building,dbo.v_Address.Appartment,dbo.v_Address.AppartmentID as AddressID from dbo.v_Address join Appartment on Appartment.AppartmentID=dbo.v_Address.AppartmentID where PersonID = @PersonID", emp, transaction: t).ConfigureAwait(false)).ToList();
                        emp.EmploymentContract = (await c.QueryAsync<EmploymentContract>("select ProfID, JobTitleID, CONVERT(VARCHAR, HiringDate, 104) as HiringDate, TerminationDate, CONVERT(VARCHAR, LeaveStartDate, 104) as LeaveStartDate, CONVERT(VARCHAR, LeaveEndDate, 104) as LeaveEndDate from ProfInfo where PersonID = @PersonID", emp, transaction: t).ConfigureAwait(false)).ToList();
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return emp;
            }).ConfigureAwait(false);
        }


        public async Task<List<Employee>> GetAll()
        {
            List<Employee> list = null;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        list = (await c.QueryAsync<Employee>("select PersonID, LastName, FirstName, Passport, CONVERT(VARCHAR, BirthDate, 104) as BirthDate, Gender, MaritalStatusID from AllStaff", transaction: t).ConfigureAwait(false)).ToList();
                        for (int i = 0; i < list.Count; i++)
                        {
                            list[i] = FillData(list[i]).Result;
                        }
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return list;
            }).ConfigureAwait(false);
        }


        public async Task<Dictionary<int, string>> MaritalStatusTable()
        {
            Dictionary<int, string> marstat = null;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        marstat = (await c.QueryAsync<KeyValue>("select ID, Status as Value from MaritalStatus", transaction: t).ConfigureAwait(false)).ToDictionary(x => x.ID, x => x.Value);
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return marstat;
            }).ConfigureAwait(false);
        }


        public async Task<List<Employee>> GetByName(string name)
        {
            string format = "%{0}%";
            name = string.Format(format, name);
            List<Employee> list = null;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        var queryResult = await c.QueryAsync<Employee>("SELECT PersonID, LastName, FirstName, Passport, CONVERT(VARCHAR, BirthDate, 104) as BirthDate, Gender, MaritalStatusID FROM AllStaff WHERE ((LastName LIKE @name) or (FirstName LIKE @name))", new { name }, transaction: t).ConfigureAwait(false);
                        list = queryResult.ToList();
                        for (int i = 0; i < list.Count; i++)
                        {
                            list[i] = FillData(list[i]).Result;
                        }
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return list;
            }).ConfigureAwait(false);
        }


        public async Task<Employee> GetById(int id)
        {
            Employee emp = null;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        emp = ( await c.QueryAsync<Employee>("select PersonID, LastName, FirstName, Passport, CONVERT(VARCHAR, BirthDate, 104) as BirthDate, Gender, MaritalStatusID from AllStaff where PersonID=@id", new { id }, transaction: t).ConfigureAwait(false)).First();
                        emp = FillData(emp).Result;
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return emp;
            }).ConfigureAwait(false);
        }


        public async Task<List<object>> GetPermissions(string obj)
        {
            List<object> permissions = null;
            string format = "SELECT * FROM fn_my_permissions ('{0}', 'OBJECT')";
            string query = string.Format(format, obj);
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        permissions = ( await c.QueryAsync(query, transaction: t).ConfigureAwait(false)).ToList();
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return permissions;
            }).ConfigureAwait(false);
        }


        public async Task<List<dynamic>> Views(string query)
        {
            List<dynamic> view = null;
            return await WithConnection(async c =>
            {
                using (var t = c.BeginTransaction())
                {
                    try
                    {
                        view = (await c.QueryAsync<dynamic>(query, transaction: t).ConfigureAwait(false)).ToList();
                        t.Commit();
                    }
                    catch (Exception e)
                    {
                        t.Rollback();
                        throw e;
                    }
                }
                return view;
            }).ConfigureAwait(false);
        }
    }
}
 