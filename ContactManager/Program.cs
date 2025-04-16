using System;
using System.Collections.Generic;
using System.IO;

namespace ContactManager
{
    class Contact
    {
        public string Name { get; set; }
        public string Phone { get; set; }

        public override string ToString()
        {
            return $"{Name},{Phone}";
        }

        public static Contact FromCsv(string line)
        {
            var parts = line.Split(',');
            return new Contact { Name = parts[0], Phone = parts[1] };
        }
    }

    internal class Program
    {
        static List<Contact> contacts = new List<Contact>();
        static string filePath = "contacts.txt";

        static void Main(string[] args)
        {
            LoadContacts();

            while (true)
            {
                Console.WriteLine("\n----- Contact Manager -----");
                Console.WriteLine("1. Add Contact");
                Console.WriteLine("2. View Contacts");
                Console.WriteLine("3. Edit Contact");
                Console.WriteLine("4. Delete Contact");
                Console.WriteLine("5. Save & Exit");
                Console.Write("Enter choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        ViewContacts();
                        break;
                    case "3":
                        EditContact();
                        break;
                    case "4":
                        DeleteContact();
                        break;
                    case "5":
                        SaveContacts();
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddContact()
        {
            Console.Write("Enter Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Phone: ");
            string phone = Console.ReadLine();

            contacts.Add(new Contact { Name = name, Phone = phone });
            Console.WriteLine("Contact added.");
        }

        static void ViewContacts()
        {
            Console.WriteLine("\n--- Contact List ---");
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found.");
                return;
            }

            for (int i = 0; i < contacts.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Name: {contacts[i].Name}, Phone: {contacts[i].Phone}");
            }
        }

        static void EditContact()
        {
            ViewContacts();
            Console.Write("Enter the number of the contact to edit: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= contacts.Count)
            {
                Console.Write("New Name (leave blank to keep same): ");
                string name = Console.ReadLine();

                Console.Write("New Phone (leave blank to keep same): ");
                string phone = Console.ReadLine();

                if (!string.IsNullOrEmpty(name))
                    contacts[index - 1].Name = name;

                if (!string.IsNullOrEmpty(phone))
                    contacts[index - 1].Phone = phone;

                Console.WriteLine("Contact updated.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        static void DeleteContact()
        {
            ViewContacts();
            Console.Write("Enter the number of the contact to delete: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= contacts.Count)
            {
                contacts.RemoveAt(index - 1);
                Console.WriteLine("Contact deleted.");
            }
            else
            {
                Console.WriteLine("Invalid index.");
            }
        }

        static void SaveContacts()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (var contact in contacts)
                {
                    writer.WriteLine(contact.ToString());
                }
            }
            Console.WriteLine("Contacts saved to file.");
        }

        static void LoadContacts()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    contacts.Add(Contact.FromCsv(line));
                }
            }
        }
    }
}
