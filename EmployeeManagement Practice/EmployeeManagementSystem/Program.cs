using System;
using System.Collections.Generic;
class Employee
{
    // Fields (Encapsulation)
    private int id;
    private string name;
    private string dept;
    private double salary;

    //properties
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    public string Name
    {
        get { return name; }
        set { name = value; }

    }
    public string Dept
    {
        get { return dept; }
        set { dept = value; }
    }
    public double Salary
    {
        get { return salary; }
        set { salary = value; }
    }
    //polymorphism
    public virtual void display()
    {
        Console.WriteLine($"Employee Id: {id}");
        Console.WriteLine($"Employee Name: {name}");
        Console.WriteLine($"Employee Department: {dept}");
        Console.WriteLine($"Employee Salary: {salary}");
    }
}
//single inheritance
    class Manager : Employee
    {
        private double bonus;
        public double Bonus
        {
            get { return bonus; }
            set { bonus = value; }
        }
        public override void display()
        {
            Console.WriteLine($"Manager Id: {Id}");
            Console.WriteLine($"Manager Name: {Name}");
            Console.WriteLine($"Manager Department: {Dept}");
            Console.WriteLine($"Manager Salary: {Salary}");
            Console.WriteLine($"Manager Bonus: {Bonus}");
        }
    }
    class Program
    {
      static  List<Employee> emp = new List<Employee>();
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. Add Employee or Manager: ");
            Console.WriteLine("2. Remove Employee or Manager: ");
            Console.WriteLine("3. View Employee or Manager: ");
            Console.WriteLine("4. Update Salary: ");
            Console.WriteLine("5. Exit");
            Console.WriteLine("--------------------------------------");

            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    AddEmployee();
                    break;
                case 2:
                    RemoveEmployee();
                    break;
                case 3:
                    ViewEmployees();
                    break;
                case 4:
                    UpdateSalary();
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid choice");
                    break;
            }
            // add employee or manager
            static void AddEmployee() {
                Console.Write("Enter 1 for Employee or 2 for Manager: ");
                int type = int.Parse(Console.ReadLine());
                if (type != 1 && type != 2)
                {
                    Console.WriteLine("Error: Invalid type! Must be 1 or 2.");
                    return; 
                }
                Console.Write("Enter id: ");
                int id = int.Parse(Console.ReadLine());

                // To add only unique id
                foreach(var e in emp)
                {
                    if(e.Id == id)
                    {
                        Console.WriteLine("Error: Id already exists TRY AGAIN!");
                        return;
                    }
                }
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                Console.Write("Enter department: ");
                string dept = Console.ReadLine();
                Console.Write("Enter Salary: ");
                double salary = double.Parse(Console.ReadLine());

                if (type == 2)
                {
                    Console.Write("Enter Manager Bonus: ");
                    double bonus = double.Parse(Console.ReadLine());
                    emp.Add(new Manager { Id = id, Name = name, Dept = dept, Salary = salary, Bonus = bonus });
                }
                else
                {
                    emp.Add(new Employee { Id = id, Name = name, Dept = dept, Salary = salary });
                    }
                Console.WriteLine("Added details Successfully!!");
            }
            // remove employee by id
            static void RemoveEmployee()
            {
                Console.Write("Enter Id of Employee/Manager to remove: ");
                int id = int.Parse(Console.ReadLine());
                bool found = false;
                for (int i = 0;i < emp.Count;i++)
                {
                    if (emp[i].Id == id)
                    {
                        emp.RemoveAt(i);
                        Console.WriteLine("Employee/Manager removed successfully!");
                        found = true;
                        break; 
                    }
                }
                if (!found)
                    Console.WriteLine("Employee/Manager not found!");
            }
            // view all employees
            static void ViewEmployees()
            {
                foreach (var e in emp)
                {
                    Console.WriteLine(" ");
                    e.display();
                }
            }
            // update employee by id
            static void UpdateSalary()
            {
                Console.Write("Enter Id of Employee/Manager to update salary: ");
                int id = int.Parse(Console.ReadLine());

                bool found = false;
                for (int i = 0; i < emp.Count; i++)
                {
                    if (emp[i].Id == id)
                    {
                        Console.Write("Enter new salary: ");
                        emp[i].Salary = double.Parse(Console.ReadLine());
                        Console.WriteLine("Salary updated successfully!");
                        found = true;
                        break; 
                    }
                }
                if (!found)
                    Console.WriteLine("Employee/Manager not found!");
            }

        }
    }
}

