using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaskTrackerClI;

namespace DesignPatterns
{
    public static class TaskCrudOperations
    {
        public static int ids = 1;
        public static string path = "C:\\Users\\iT\\source\\repos\\DesignPatterns\\DesignPatterns\\task.json";
        public static void Add_Task(Tasks task)
        {


            task.Id = ids++;
            List<Tasks> tasks = new List<Tasks>();


            var jsonText = JsonSerializer.Serialize<Tasks>(task);




            if (File.Exists(path))
            {

                var texts = File.ReadAllLines(path);

                for (int i = 0; i < texts.Length; ++i)
                {
                    if (!string.IsNullOrEmpty(texts[i]))
                    {
                        var task2 = JsonSerializer.Deserialize<Tasks>(texts[i]);
                        tasks.Add(task2!);
                    }
                }
                tasks.Add(task);



                string jsons = "";

                foreach (var t in tasks)
                {
                    jsons += JsonSerializer.Serialize(t);
                    jsons += "\n";
                }
                File.WriteAllText(path, jsons);
            }
            else
            {
                File.Create(path);

                File.WriteAllLines(path, new string[] { JsonSerializer.Serialize(task, new JsonSerializerOptions { WriteIndented = true }) });
            }
        }
        public static void update_Task(int id, string Newdescription)
        {

            var text = File.ReadAllLines(path);



            List<Tasks> tasks = new List<Tasks>();

            foreach (var t in text)
            {
                if (!string.IsNullOrEmpty(t))
                {
                    var taskObj = JsonSerializer.Deserialize<Tasks>(t);
                    tasks.Add(taskObj);
                }
            }

            var updatedTask = tasks.FirstOrDefault(x => x.Id == id);

            if (updatedTask is not null)
            {
                updatedTask.Description = Newdescription;
                updatedTask.UpdatedAt = DateTime.Now;
            }
            var str = "";
            foreach (var task in tasks)
            {
                str += JsonSerializer.Serialize(task) + "\n";

                File.WriteAllLines(path, new string[] { str });
            }
        }
        public static void delete_Task(int id)
        {
            var text = File.ReadAllLines(path);

            List<Tasks> tasks = new List<Tasks>();


            foreach (var t in text)
            {
                if (!string.IsNullOrEmpty(t))
                {
                    var taskObj = JsonSerializer.Deserialize<Tasks>(t);
                    tasks.Add(taskObj);
                }
            }




            var deletedTask = tasks.Find(x => x.Id == id);

            if (deletedTask is not null)
                tasks.Remove(deletedTask);

            var str = "";
            foreach (var task in tasks)
            {
                str += JsonSerializer.Serialize(task) + "\n";


                File.WriteAllLines(path, new string[] { str });
            }
        }
        public static void mark_Task(int id, string status)
        {
            var text = File.ReadAllLines(path);



            List<Tasks> tasks = new List<Tasks>();


            foreach (var t in text)
            {
                if (!string.IsNullOrEmpty(t))
                {
                    var taskObj = JsonSerializer.Deserialize<Tasks>(t);
                    tasks.Add(taskObj!);
                }
            }
            var markedTask = tasks.FirstOrDefault(x => x.Id == id);

            if (markedTask is not null)
            {
                markedTask.status = status;
                markedTask.UpdatedAt = DateTime.Now;
            }

            string str = "";
            foreach (var task in tasks)
            {
                str += JsonSerializer.Serialize(task) + "\n";
                File.WriteAllLines(path, new string[] { str });
            }
        }
        public static void list()
        {
            var text = File.ReadAllLines(path);



            List<Tasks> tasks = new List<Tasks>();

            foreach (var t in text)
            {
                if (!string.IsNullOrEmpty(t))
                {
                    var taskObj = JsonSerializer.Deserialize<Tasks>(t);
                    tasks.Add(taskObj);
                }
            }

            var result = ""; 
            foreach (var task in tasks)
            {
                result += JsonSerializer.Serialize(task , new JsonSerializerOptions {  WriteIndented = true})+"\n";
            }
            Console.WriteLine(result);
        }
        public static void list_Tasks(string status)
        {
            var text = File.ReadAllLines(path);



            List<Tasks> tasks = new List<Tasks>();

            foreach (var t in text)
            {
                if (!string.IsNullOrEmpty(t))
                {
                    var taskObj = JsonSerializer.Deserialize<Tasks>(t);
                    tasks.Add(taskObj);
                }
            }
            string result = "";
            foreach (var task in tasks)
            {
                if (task.status == status)
                {
                    result += JsonSerializer.Serialize(task, new JsonSerializerOptions { WriteIndented = true })+"\n";
                }
            }
            Console.WriteLine(result);
        }



    }

}
