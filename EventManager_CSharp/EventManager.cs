﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace EventManager_CSharp
{
    class EventManager
    {
        private static EventManager instance;

        /// <summary>
        /// Default Constructor.
        /// </summary>
        private EventManager()
        {
        }

        /// <summary>
        /// Singleton pattern instantiation of the EventManager object.
        /// Returns the unique instance of the EventManager object.
        /// </summary>
        public static EventManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new EventManager();
                return instance;
            }
        }

        /// <summary>
        /// Method waits for input and takes actions according to that input.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("EventManager started");
            string _input = "";
            while (_input != "quit")
            {
                Console.Write(">");
                _input = Console.ReadLine();
                if (_input == "le")
                {
                    le();
                }
                else if (_input.StartsWith("add"))
                {
                    add(_input);
                }
                else if (_input.StartsWith("remove"))
                {
                    remove(_input);
                }
                else if (_input == "clear")
                {
                    clear();
                }
                else if (_input == "help")
                {
                    help();
                }
                else if (_input.StartsWith("register"))
                {
                    register(_input);
                }
                else if (_input.StartsWith("drop"))
                {
                    drop(_input);
                }
                else if (_input.StartsWith("modify"))
                {
                    modify(_input);
                }
                else if (_input == "quit")
                {
                    Console.Out.WriteLine("EventManager is terminating...");
                }
                else
                {
                    Console.Out.WriteLine("Illegal command: " + _input);
                }
            }
        }

        /// <summary>
        /// The method modifies the given event locally as well as remote
        /// </summary>
        /// <param name="_input">The input string</param>
        private void modify(string _input)
        {
            string[] _tokens = _input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (_tokens.Count<string>() < 4)
            {
                Console.Out.WriteLine("Illegal arguments");
                Console.Out.WriteLine("modify [id] [field] [newValue] --> Modify the event with the given id and set the value of the specified field to [newValue].\r\n");
                return;
            }
            string _id = _tokens[1];
            string _field = _tokens[2];
            string _newValue = _tokens[3];

            StreamReader _sr = new StreamReader(File.Open(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\..\\..\\events\\" + _id + ".event"), FileMode.Open, FileAccess.Read));
            string _s = _sr.ReadLine();
            _sr.Close();
            string[] _fields = _s.Split(new string[] { "\t" }, StringSplitOptions.RemoveEmptyEntries);
            DateTime _datetime;
            switch (_field)
            {
                case "date":
                    string[] _date = _newValue.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        _datetime = new DateTime(Int32.Parse(_date[2]), Int32.Parse(_date[1]), Int32.Parse(_date[0]), 0, 0, 0);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid date given");
                        return;
                    }
                    _fields[1] = _datetime.Day + "." + _datetime.Month + "." + _datetime.Year;
                    break;
                case "time":
                    string[] _time = _newValue.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);
                    try
                    {
                        _datetime = new DateTime(1, 1, 1, Int32.Parse(_time[0]), Int32.Parse(_time[1]), 0);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid time given");
                        return;
                    }
                    _fields[2] = _datetime.ToShortTimeString();
                    break;
                case "duration":
                    try
                    {
                        Int32.Parse(_fields[3]);
                    }
                    catch
                    {
                        Console.WriteLine("Invalid duration given. Should be an integer");
                        return;
                    }
                    _fields[3] = _newValue;
                    break;
                case "header":
                    _fields[4] = _newValue;
                    break;
                case "comment":
                    _fields[5] = _newValue;
                    break;
                default:
                    Console.WriteLine("Wrong field name given");
                    return;
            }
            _s = String.Join("\t", _fields);
            StreamWriter _sw = new StreamWriter(File.Open(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\..\\..\\events\\" + _id + ".event"), FileMode.Create, FileAccess.Write));
            _sw.WriteLine(_s);
            _sw.Close();
            //ToDo: Modify the given event remote
        }

        /// <summary>
        /// Method delete the given node from the list of remote hosts
        /// </summary>
        /// <param name="_input">The input string</param>
        private void drop(string _input)
        {
            string[] _tokens = _input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (_tokens.Count<string>() < 2)
            {
                Console.Out.WriteLine("Illegal arguments");
                Console.Out.WriteLine("drop [ip]: Remove the host with the given IP address from the list of remote hosts. \r\n");
                return;
            }
            //ToDo: Add additional input validity checks
            string _ip = _tokens[1];            
            //ToDo: Remove the host with the given IP from the list of remote hosts and notify the remote host for mutual deletion.
        }

        private void register(string _input)
        {
            string[] _tokens = _input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (_tokens.Count<string>() < 2)
            {
                Console.Out.WriteLine("Illegal arguments");
                Console.Out.WriteLine("register [ip]: Register the host with the given IP address as remote host.\r\n");
                return;
            }
            //ToDo: Add additional input validity checks
            string _ip = _tokens[1];  
            //ToDo: Add the host with the given IP to the list of remote hosts and notify the remote host for mutual addition. 
        }

        /// <summary>
        /// Method to lists all events
        /// </summary>
        private void le()
        {

            DirectoryInfo _di = new DirectoryInfo(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\..\\..\\events"));
            foreach (FileInfo _fi in _di.GetFiles())
            {
                StreamReader _sr = new StreamReader(File.Open(_fi.FullName, FileMode.Open, FileAccess.Read));
                String _s = _sr.ReadLine();
                _sr.Close();
                Console.WriteLine(_s);
            }
        }

        /// <summary>
        /// Method to add a new event locally as well as remote
        /// </summary>
        /// <param name="_input">The input string</param>
        private void add(string _input)
        {
            string[] _tokens = _input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (_tokens.Count<string>() < 6)
            {
                Console.Out.WriteLine("Illegal arguments");
                Console.Out.WriteLine("add [date] [time] [duration] [header] [comment] --> add a new event with the given arguments; date format: dd.mm.yyyy; time format: hh:mm; duration in minutes;\r\n");
                return;
            }

            string[] _date = _tokens[1].Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
            string[] _time = _tokens[2].Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries);

            //Date validity check
            try
            {
                new DateTime(Int32.Parse(_date[2]), Int32.Parse(_date[1]), Int32.Parse(_date[0]), 0, 0, 0);
            }
            catch
            {
                Console.WriteLine("Invalid date given");
                return;
            }

            //Time validity check
            try
            {
                new DateTime(1, 1, 1, Int32.Parse(_time[0]), Int32.Parse(_time[1]), 0);
            }
            catch
            {
                Console.WriteLine("Invalid time given");
                return;
            }

            //Duration validity check
            try
            {
                Int32.Parse(_tokens[3]);
            }
            catch
            {
                Console.WriteLine("Invalid duration given. Should be an integer");
                return;
            }

            DateTime _datetime = new DateTime(Int32.Parse(_date[2]), Int32.Parse(_date[1]), Int32.Parse(_date[0]), Int32.Parse(_time[0]), Int32.Parse(_time[1]), 0);
            int _duration = Int32.Parse(_tokens[3]);
            string _header = _tokens[4];
            string _comment = _tokens[5];

            DirectoryInfo _di = new DirectoryInfo(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\..\\..\\events"));
            int _id = 0;
            foreach (FileInfo _fi in _di.GetFiles())
            {
                string fileName = _fi.Name.Substring(0, _fi.Name.Length - ".event".Length);
                int fileNum = Int32.Parse(fileName);
                if (fileNum >= _id)
                    _id = fileNum + 1;
            }
            string newEventFileContent = _id.ToString() + "\t" + _datetime.Day + "." + _datetime.Month + "." + _datetime.Year + "\t" + _datetime.ToShortTimeString() + "\t" + _duration.ToString() + "\t" + _header + "\t" + _comment;
            string newEventFilePath = "..\\..\\..\\events\\"+_id.ToString()+".event";
            StreamWriter _sw = new StreamWriter(File.Open(newEventFilePath, FileMode.CreateNew, FileAccess.Write));
            _sw.WriteLine(newEventFileContent);
            _sw.Close();
            // ToDo: send the newly created file to the other devices...
        }

        /// <summary>
        /// Method to remove the given event locally as well as remote
        /// </summary>
        /// <param name="_input">The input string</param>
        private void remove(string _input)
        {
            string[] _tokens = _input.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (_tokens.Count<string>() < 2)
            {
                Console.Out.WriteLine("Illegal arguments");
                Console.Out.WriteLine("remove [id] --> remove the event with the given id;\r\n");
                return;
            }
            //ToDo: Add additional input validity checks
            try
            {
                int _id = Int32.Parse(_tokens[1]);
            }
            catch
            {
                Console.WriteLine("Invalid event ID given. Should be an integer.");
                return;
            }

            File.Delete(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\..\\..\\events\\" + _tokens[1] + ".event"));
            //ToDo: Notify other devices
        }

        /// <summary>
        /// Method to remove all events locally as well as remote
        /// </summary>
        private void clear()
        {
            DirectoryInfo _di = new DirectoryInfo(System.IO.Path.Combine(System.Windows.Forms.Application.StartupPath, "..\\..\\..\\events"));
            foreach (FileInfo _fi in _di.GetFiles())
            {
                File.Delete(_fi.FullName);
            }
            //ToDo: Notify other devices 
        }

        /// <summary>
        /// Method to print all valid commands and arguments to the screen 
        /// </summary>
        private void help()
        {
            Console.Out.WriteLine("le: List all events.\r\n");
            Console.Out.WriteLine("add [date] [time] [duration] [header] [comment]: Add a new event with the given arguments. date format: dd.mm.yyyy; time format: hh:mm; duration in minutes;\r\n");
            Console.Out.WriteLine("clear: Remove all events.\r\n");
            Console.Out.WriteLine("drop [ip]: Remove the host with the given IP address from the list of remote hosts. \r\n");
            Console.Out.WriteLine("modify [id] [field] [value]: Modify the event with the given id and set the value of the specified field to [value].\r\n");
            Console.Out.WriteLine("register [ip]: Register the host with the given IP address as remote host.\r\n");
            Console.Out.WriteLine("remove [id]: Remove the event with the given id.\r\n");
            Console.Out.WriteLine("quit: Quit the application.\r\n");
        }
    }
}
