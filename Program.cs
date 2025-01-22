/* Kate Fairbanks
 * 
 * This program helps a food bank manage their inventory. Users are allowed to add, delete, and view inventory. When they are finished, they can exit the program.
 */
using System;

class Program
{
    static void Main(string[] args)
    {
        // Create a list to store food items
        List<FoodItem> foodItems = new List<FoodItem>();

        // Declare variable to store user input
        string userInput = "";

        // Main program loop, continues until the user chooses to exit
        while (userInput != "4")
        {
            // Display menu options
            Console.WriteLine("1- Add food item \n2- Delete food item \n3- Print list of current food items \n4- Exit the program");
            Console.Write("Please enter the number for the task you would like to complete: ");
            userInput = Console.ReadLine();

            switch (userInput)
            {
                case "1":
                    // Add a new food item
                    AddFoodItem(foodItems);
                    break;

                case "2":
                    // Delete an existing food item
                    DeleteFoodItem(foodItems);
                    break;

                case "3":
                    // Print the list of food items
                    PrintFoodItems(foodItems);
                    break;

                case "4":
                    // Exit the program
                    Environment.Exit(0);
                    break;

                default:
                    // Handle invalid input
                    Console.WriteLine("Invalid input, please enter a number between 1 and 4");
                    break;
            }
        }
    }

    // Method to add a food item to the inventory
    static void AddFoodItem(List<FoodItem> foodItems)
    {
        // Prompt for and read the name of the food item
        Console.Write("Enter food name (e.g., Canned Beans): ");
        string name = Console.ReadLine();

        // Prompt for and read the category of the food item
        Console.Write("Enter food category (e.g., Canned Goods): ");
        string category = Console.ReadLine();

        // Prompt for and validate the quantity of the food item
        int quantity;
        while (true)
        {
            Console.Write("Enter food quantity (e.g., 15): ");
            if (int.TryParse(Console.ReadLine(), out quantity) && quantity > 0)
            {
                break; // Valid quantity
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a positive integer.");
            }
        }

        // Prompt for and validate the expiration date of the food item
        DateTime expirationDate;
        while (true)
        {
            Console.Write("Enter expiration date (yyyy-mm-dd): ");
            if (DateTime.TryParse(Console.ReadLine(), out expirationDate))
            {
                break; // Valid date
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid date in yyyy-mm-dd format.");
            }
        }

        // Check if an item with the same name and expiration date already exists
        var existingItem = foodItems.Find(item =>
            item.foodName.Equals(name, StringComparison.OrdinalIgnoreCase) &&
            item.foodExpireDate == expirationDate);

        if (existingItem != null)
        {
            // Update the quantity of the existing item
            existingItem.foodQuantity += quantity;
            Console.WriteLine($"Updated quantity for {existingItem.foodName} (Exp: {existingItem.foodExpireDate.ToShortDateString()}): {existingItem.foodQuantity}");
        }
        else
        {
            // Add a new item to the inventory
            foodItems.Add(new FoodItem(name, category, quantity, expirationDate));
            Console.WriteLine("Food item added successfully!");
        }
    }

    // Method to delete a food item from the inventory
    static void DeleteFoodItem(List<FoodItem> foodItems)
    {
        // Print the current inventory for reference
        PrintFoodItems(foodItems);

        // Prompt for the name of the item to delete
        Console.Write("Enter the name of the food item you want to delete from: ");
        string name = Console.ReadLine();

        // Find the item in the inventory
        var foodItem = foodItems.Find(item => item.foodName.Equals(name, StringComparison.OrdinalIgnoreCase));

        if (foodItem != null)
        {
            int quantityToDelete;

            // Prompt for and validate the quantity to delete
            while (true)
            {
                Console.Write($"Enter the quantity to delete (current quantity: {foodItem.foodQuantity}): ");
                if (int.TryParse(Console.ReadLine(), out quantityToDelete) && quantityToDelete > 0)
                {
                    if (quantityToDelete <= foodItem.foodQuantity)
                    {
                        // Decrease the quantity or remove the item if it reaches zero
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
                        break;
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

    // Method to print the list of food items
    static void PrintFoodItems(List<FoodItem> foodItems)
    {
        if (foodItems.Count == 0)
        {
            Console.WriteLine("No food items to display.");
        }
        else
        {
            // Print a table of food items
            Console.WriteLine("{0,-20} | {1,-15} | {2,-10} | {3,-15}",
                "Food Name", "Category", "Quantity", "Expiration Date");
            Console.WriteLine(new string('-', 65));

            foreach (var item in foodItems)
            {
                Console.WriteLine("{0,-20} | {1,-15} | {2,-10} | {3,-15}",
                    item.foodName, item.foodCategory, item.foodQuantity, item.foodExpireDate.ToShortDateString());
            }
        }
    }
}

class FoodItem
{
    // Properties of a food item
    public string foodName { get; set; }
    public string foodCategory { get; set; }
    public int foodQuantity { get; set; }
    public DateTime foodExpireDate { get; set; }

    // Constructor for creating a FoodItem object
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
