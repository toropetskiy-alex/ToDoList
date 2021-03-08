using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Services
{
    class FileIOservice
    {
        private readonly  string PATH;
        public FileIOservice(string path)
        {
            PATH = path;
        }
        public BindingList<TodoModel> LoadData()
        {
            // Проверяем, существует ли файл? 
            var fileExists = File.Exists(PATH);
            if (!fileExists)
            {
                // Если файла не существует - создаем файл, передает путь 
                File.CreateText(PATH).Dispose();
                return new BindingList<TodoModel>();
            }
            // Если файл уже существовал, выполняем чтение из файла
            using (var reader = File.OpenText(PATH))
            {
                var fileText = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<TodoModel>>(fileText);
            }
        }
        public void SaveData(object todoDataList)
        {
            using (StreamWriter writer = File.CreateText(PATH))
            {

                string output = JsonConvert.SerializeObject(todoDataList);
                writer.Write(output);
            }
            
        } 
    }
}
