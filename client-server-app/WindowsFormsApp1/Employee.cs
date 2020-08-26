using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace WindowsFormsApp1
{
    public class KeyValue
    {
        public int ID { get; set; }
        public string Value { get; set; }
    }
    public class EmploymentContract
    {
        public int ProfID { get; set; }
        public int JobTitleID { get; set; }
        public string HiringDate { get; set; }
        public string TerminationDate { get; set; }
        public string LeaveStartDate { get; set; }
        public string LeaveEndDate { get; set; }
    }
    public class Child
    {
        public int ChildID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Gender { get; set; } 
        public string BirthDate { get; set; }
    }
    public class EmergencyContact
    {
        public int EmergencyContactID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string CellPhone { get; set; }
    }
    public class Address
    {
        public int AddressID { get; set; }
        public int Appartment { get; set; }
        public string Building { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    } 
    public class Employee
    {
        public int PersonID { get; set; }
        public string LastName { get; set; } 
        public string FirstName { get; set; } 
        public string Passport { get; set; }  
        public string BirthDate { get; set; }
        public string Gender { get; set; }
        public int MaritalStatusID { get; set; }
        public List<KeyValue> CellPhone { get; set; }
        public List<KeyValue> Email { get; set; } 
        public List<EmergencyContact> EmergencyContact { get; set; }
        public List<Child> Children { get; set; }
        public List<EmploymentContract> EmploymentContract { get; set; }
        public List<Address> Address { get; set; }
    }
}
