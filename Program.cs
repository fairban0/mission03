using System;
using System.ComponentModel;

class Program
{
    static void Main(string[] args)
    {
        

        List<FoodItem> foodItems = new List<FoodItem>();
        string userInput = "";

 
        while (userInput != "4")
            {
                Console.WriteLine("1- Add food item \n2- Delete food item \n3- Print list of current food items \n4- Exit the program");
                Console.Write("Please enter the number for the task you would like to complete: ");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    // Add food items (through the constructor)
                    case "1":
                        AddFoodItem(foodItems);
                        break;

                    // Delete food items
                    case "2":
                        DeleteFoodItem(foodItems);
                        break;

                    // Print list of current food items (loop)
                    case "3":
                        PrintFoodItems(foodItems);
                        break;

                    // Exit the program 
                    case "4":
                        Console.WriteLine("The user entered 4");
                        break;
                    default:
                        Console.WriteLine("Invalid input, please enter a number between 1 and 4");
                        break;
            }
        };

        static void AddFoodItem(List<FoodItem> foodItems)
        {
            Console.Write("Enter food name (e.g., Canned Beans): ");
            string name = Console.ReadLine();

            Console.Write("Enter food category (e.g., Canned Goods): ");
            string category = Console.ReadLine();

            int quantity;
            while (true)
            {
                Console.Write("Enter food quantity (e.g., 15): ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
                {
                    break; // Valid integer input; exit the loop
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a positive integer.");
                }
            }

            DateTime expirationDate;
            while (true)
            {
                Console.Write("Enter expiration date (yyyy-mm-dd): ");
                if (DateTime.TryParse(Console.ReadLine(), out expirationDate))
                {
                    break; // Valid date input; exit the loop
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid date in yyyy-mm-dd format.");
                }
            }

            FoodItem newFoodItem = new FoodItem(name, category, quantity, expirationDate);

            foodItems.Add(newFoodItem);

            Console.WriteLine("Food item added successfully!");
        }


        static void DeleteFoodItem(List<FoodItem> foodItems)
        {
            PrintFoodItems(foodItems);

            Console.Write("Enter the name of the food item you want to delete from: ");
            string name = Console.ReadLine();

            var foodItem = foodItems.Find(item => item.foodName.Equals(name, StringComparison.OrdinalIgnoreCase));

            if (foodItem != null)
            {
                int quantityToDelete;

                while (true)
                {
                    Console.Write($"Enter the quantity to delete (current quantity: {foodItem.foodQuantity}): ");

                    if (int.TryParse(Console.ReadLine(), out quantityToDelete) && quantityToDelete > 0)
                    {
                        if (quantityToDelete <= foodItem.foodQuantity)
                        {
                            // Valid quantity; proceed with deletion
                            foodItem.foodQuantity -= quantityToDelete;

                            if (foodItem.foodQuantity == 0)
                            {
                                foodItems.Remove(foodItem);
                                Console.WriteLine("Food item completely removed from the list.");
                            }
                            else
                            {
                                Console.WriteLine($"Updated quantity for {foodItem.foodName}: {foodItem.foodQuantity}");
                            }

                            break; // Exit the loop after successful deletion
                        }
                        else
                        {
                            Console.WriteLine("Quantity exceeds the current stock. Please try again.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a positive integer.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Food item not found.");
            }

        }

        static void PrintFoodItems(List<FoodItem> foodItems)
        {
            if (foodItems.Count == 0)
            {
                Console.WriteLine("No food items to display.");
            }
            else
            {
                // Print table headers
                Console.WriteLine("{0,-20} | {1,-15} | {2,-10} | {3,-15}",
                    "Food Name", "Category", "Quantity", "Expiration Date");
                Console.WriteLine(new string('-', 65));

                // Print each food item in a formatted table
                foreach (var item in foodItems)
                {
                    Console.WriteLine("{0,-20} | {1,-15} | {2,-10} | {3,-15}",
                        item.foodName, item.foodCategory, item.foodQuantity, item.foodExpireDate.ToShortDateString());
                }
            }
        }


    }
}
class FoodItem
{
    // stores the information for an individual food item

    // Name (e.g., "Canned Beans")
    public string foodName { get; set; }
   
    // Category (e.g., "Canned Goods", "Dairy", "Produce")
    public string foodCategory { get; set; }

    // Quantity (e.g., "15 units")
    public int foodQuantity { get; set; }

    // Expiration Date
    public DateTime foodExpireDate { get; set; }

    // Constructor to initialize a FoodItem object
    public FoodItem(string name, string category, int quantity, DateTime expireDate)
    {
        foodName = name;
        foodCategory = category;
        foodQuantity = quantity;
        foodExpireDate = expireDate;
    }

    public override string ToString()
    {
        return $"{foodName} - {foodCategory} - {foodQuantity} - {foodExpireDate.ToShortDateString()}";
    }
}
