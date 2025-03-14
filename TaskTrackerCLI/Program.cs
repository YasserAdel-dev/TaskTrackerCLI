using DesignPatterns;
using Newtonsoft.Json;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Channels;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static TaskTrackerClI.Program;

namespace TaskTrackerClI
{
    internal class Program
    {

        static void Main(string[] args)
        { 
            Run();
        }


        public static void Run()
        {
            while(true)
            {
                Console.Write("task-cli ");
                string str =  Console.ReadLine();

                var tasks = str.Split(" ");

                if (tasks[0] == "add")
                {

                    string result = string.Join(" ", tasks.Skip(1));

                    Tasks task = new Tasks
                    {
                        Description = result,
                        status = "todo",
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now,
                    };
                    TaskCrudOperations.Add_Task(task);
                }
                else if (tasks[0] == "delete")
                {
                    TaskCrudOperations.delete_Task(Convert.ToInt32(tasks[1]));
                }
                else if (tasks[0] == "update")
                {
                    string result = string.Join(" ", tasks.Skip(2));

                    Console.WriteLine(result);
                    TaskCrudOperations.update_Task(Convert.ToInt32(tasks[1]), result);
                }
                else if (tasks[0] == "mark-in-progress")
                {
                    TaskCrudOperations.mark_Task(Convert.ToInt32(tasks[1]), "in_progress");
                }
                else if (tasks[0] == "mark-done")
                {
                    TaskCrudOperations.mark_Task(Convert.ToInt32(tasks[1]), "done");
                }
                else if (tasks[0] == "list")
                {
                    if (tasks.Length == 1)
                        TaskCrudOperations.list();
                    else
                        TaskCrudOperations.list_Tasks(tasks[1]);
                }
                else if (tasks[0] == "cls" || tasks[0] == "clear")
                    Console.Clear();
            }
        }
    }



   
    public class Tasks
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string status { get; set; }
        public DateTime CreatedAt {get;set;}

        public DateTime UpdatedAt { get; set;}

        public override string ToString()
        {
            return $"{Id} , {Description} , {status} , {CreatedAt} , {UpdatedAt}";
        }
    }
}
