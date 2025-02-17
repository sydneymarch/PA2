//begin main
const double JAPANESE_YEN = 0.0064;
const double CHINESE_YUAN = 0.14;
const double BRITISH_POUND = 1.22;
const double DOUBLOONS = 8.40;
const double ALGERIAN_DINAR = 0.0074;
const double SENT_VALUE = -99999;
//setting all these constants to be used in all the methods and main and easily be changed with fluctuating exchange rates'
//begin main
string userInput = GetMainMenuInput(); // initial read
while (userInput != "3") { // loop until exit
    ProcessMainMenuChoice(userInput); // goes down line of choice
    userInput = GetMainMenuInput(); // update read
}
ProcessMainMenuChoice(userInput); // fencepost check
//end main


// displays the main menu and returns the user's input
static string GetMainMenuInput() {
    ClearScreen();
    Console.WriteLine("Hello welcome to Pirate Shipping Co.\n1. Convert Currency\n2. Shipping Invoice\n3. Exit");
    return Console.ReadLine();
}

// Processes the main menu selection
static void ProcessMainMenuChoice(string userInput) {
    switch (userInput) {
        case "1":
            ConvertCurrency();
            break;
        case "2":
            ShippingInvoice();
            break;
        case "3":
            Console.WriteLine("Goodbye");
            break;
        default:
            Console.WriteLine($"Invalid input '{userInput}'.");
            Pause();
            break;
    }
}

//handles the entire currency conversion process
static void ConvertCurrency() {
    ClearScreen();
    string conversionOption = GetConversionOption(); //gets conversion direction

    //loop until the user opts to go back
    while (conversionOption != "3") {
        if (conversionOption == "1") {// Convert TO USD
            ConvertToUSDFlow();
        }
        else if (conversionOption == "2") {// Convert FROM USD
            ConvertFromUSDFlow();
        }
        else {
            Console.WriteLine("Invalid option. Please try again.");
            Pause();
        }
        conversionOption = GetConversionOption();
    }
}

//prompt for conversion direction
static string GetConversionOption() {
    ClearScreen();
    Console.WriteLine("Would you like to convert to USD or from USD?");
    Console.WriteLine("1. Convert to USD\n2. Convert from USD\n3. Exit to main menu");
    return Console.ReadLine();
}

//for converting a foreign currency to USD
static void ConvertToUSDFlow() {
    //prompt the user for the amount in foreign currency
    //type "back" to cancel the conversion
    ClearScreen();
    double amountInForeign = GetAmount("Enter the amount in the foreign currency you want to convert into USD (or type 'back' to cancel):", true);
    if (amountInForeign == SENT_VALUE) {
        //sentinel encountered exit
        return;
    }
    ClearScreen();
    Console.WriteLine("Select the currency you want to convert from into USD:");
    string currencyOption = GetCurrency();
    double amountInUSD = ConvertToUSD(ref currencyOption, amountInForeign);
    if (amountInUSD == SENT_VALUE) {
        Console.WriteLine($"Invalid currency option '{currencyOption}'.");
        Pause();
        return;
    }
    DisplayConvertedMessageToUSD(currencyOption, amountInForeign, amountInUSD);
}

// Flow for converting USD to a foreign currency
static void ConvertFromUSDFlow()
{
    // Prompt the user for the amount in USD;
    // type "back" to cancel the conversion
    ClearScreen();
    double amountInUSD = GetAmount("Enter the amount in USD you want to convert into a foreign currency (or type 'back' to cancel):", true);
    if (amountInUSD == SENT_VALUE){
        //exit conversion
        return;
    }
    ClearScreen();
    Console.WriteLine("Select the currency you want to convert to from USD:");
    string currencyOption = GetCurrency();
    double amountInForeign = ConvertFromUSD(ref currencyOption, amountInUSD);
    if (amountInForeign == SENT_VALUE){
        Console.WriteLine($"Invalid currency option '{currencyOption}'.");
        Pause();
        return;
    }
    DisplayConvertedMessageFromUSD(currencyOption, amountInUSD, amountInForeign);
}

// Prompts the user for an amount the user may type "back" to cancel the operation returns the valid number
static double GetAmount(string message, bool allowNegative){
    // Display the prompt message.
    ClearScreen();
    Console.WriteLine(message);
    string input = Console.ReadLine();

    //if the user types back, return the sentinel value
    if (allowNegative) {
        if (input.ToLower() == "back"){
            return SENT_VALUE;
        }
        //if the input is a valid number, return it
        if (double.TryParse(input, out double result)){
            return result;
        }
        else {
            //recursively prompt again
            Console.WriteLine("Invalid input. Please enter a valid number or type 'back' to cancel:");
            Pause();
            return GetAmount(message, allowNegative);
        }
    }
    else {
        if (input.ToLower() == "back") {
            return SENT_VALUE;
        }
        if (double.TryParse(input, out double result)) {
            if (result < 0) {
                Console.WriteLine("Invalid input. Please enter a valid number greater than 0 or type 'back' to cancel:");
                Pause();
                return GetAmount(message, allowNegative);
            }
            return result;
        }
        else {
            //recursively prompt again
            Console.WriteLine("Invalid input. Please enter a valid number or type 'back' to cancel:");
            Pause();
            return GetAmount(message, allowNegative);
        }
    }

}


// Displays the currency options and returns the user's choice
static string GetCurrency(){
    Console.WriteLine("1. US Dollar\n2. Japanese Yen\n3. Chinese Yuan\n4. British Pound\n5. Doubloons\n6. Algerian Dinar");
    return Console.ReadLine();
}

// Converts a foreign currency amount to USD
static double ConvertToUSD(ref string currency, double amount){
    switch (currency){
        case "1":
            currency = "US Dollars";
            return Math.Round(amount, 2);
        case "2":
            currency = "Japanese Yens";
            return Math.Round(amount * JAPANESE_YEN, 2);
        case "3":
            currency = "Chinese Yuans";
            return Math.Round(amount * CHINESE_YUAN, 2);
        case "4":
            currency = "British Pounds";
            return Math.Round(amount * BRITISH_POUND, 2);
        case "5":
            currency = "Doubloons";
            return Math.Round(amount * DOUBLOONS, 2);
        case "6":
            currency = "Algerian Dinars";
            return Math.Round(amount * ALGERIAN_DINAR, 2);
        default:
            return SENT_VALUE;
    }
}

// Converts a USD amount to a foreign currency
static double ConvertFromUSD(ref string currency, double amount){
    switch (currency){
        case "1":
            currency = "US Dollars";
            return Math.Round(amount, 2);
        case "2":
            currency = "Japanese Yens";
            return Math.Round(amount / JAPANESE_YEN, 2);
        case "3":
            currency = "Chinese Yuan";
            return Math.Round(amount / CHINESE_YUAN, 2);
        case "4":
            currency = "British Pounds";
            return Math.Round(amount / BRITISH_POUND, 2);
        case "5":
            currency = "Doubloons";
            return Math.Round(amount / DOUBLOONS, 2);
        case "6":
            currency = "Algerian Dinars";
            return Math.Round(amount / ALGERIAN_DINAR, 2);
        default:
            return SENT_VALUE;
    }
}

// Displays the conversion result when converting TO USD
static void DisplayConvertedMessageToUSD(string currency, double foreignAmount, double usdAmount){
    ClearScreen();
    Console.WriteLine($"{foreignAmount} {currency} is {usdAmount} US Dollars.");
    Pause();
}

// Displays the conversion result when converting FROM USD
static void DisplayConvertedMessageFromUSD(string currency, double usdAmount, double foreignAmount){
    ClearScreen();
    Console.WriteLine($"{usdAmount} US Dollars is {foreignAmount} {currency}.");
    Pause();
}

static void ShippingInvoice () {
    ClearScreen();
    double weight = GetAmount("Please enter the weight of the item or container in tons: (or type 'back' to exit)", false);
    if (weight == SENT_VALUE) {
        return;
    }
    double price = GetPrice(weight);
    double perishable = GetPerishable();
    if (perishable == SENT_VALUE) {
        return;
    }
    double express = GetExpress();
    if (express == SENT_VALUE) {
        return;
    }
    double finalPrice = GetFinalPrice(weight, perishable, express);

    DisplayFinalPrice(finalPrice);
    //must prompt use for the weight of the item or container, each ton is worth $220.40
    //parcels with pershiable items add $230 per ton
    //the system must add 25% increase in price to shipment that have been marked as express shipping
}
static double GetPrice(double weight) {
    return weight * 220.40;
}
static double GetFinalPrice(double weight, double perishable, double express) {
    double price = weight * 220.40;
    if (perishable == 1) {
        price += weight * 230;
    }
    if (express == 1) {
        price *= 1.25;
    }
    return price;
}
static double GetPerishable() {
    ClearScreen();
    System.Console.WriteLine("Is the item perishable?\n1. Yes\n2. No\n3. Back");
    string input = Console.ReadLine();
    if (input == "1") {
        return 1;
    }
    else if (input == "2") {
        return 0;
    }
    else if (input == "3") {
        return SENT_VALUE;
    }
    else {
        System.Console.WriteLine("Invalid input. Please try again.");
        Pause();
        return GetPerishable();
    }
}
static double GetExpress() {
    ClearScreen();
    System.Console.WriteLine("Do you want to purchase express shipping for a 25% increase to the price?\n1. Yes\n2. No\n3. Back");
    string input = Console.ReadLine();
    if (input == "1") {
        return 1;
    }
    else if (input == "2") {
        return 0;
    }
    else if (input == "3") {
        return SENT_VALUE;
    }
    else {
        System.Console.WriteLine("Invalid input. Please try again.");
        Pause();
        return GetExpress();
    }
}
static void DisplayFinalPrice(double finalPrice) {
    System.Console.WriteLine($"The final price is {finalPrice}.");
    Pause();
}
static void ClearScreen() {
    Console.Clear();
}
static void Pause() {
    System.Console.WriteLine("Press any key to continue...");
    System.Console.ReadKey();
}