/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: 
        // Expected Result: 
        // Enqueue one customer and dequeue them
        Console.WriteLine("Test 1: Add one customer, then serve them"); 
        var cs1 = new CustomerService(5); // max size 5
        Console.SetIn(new System.IO.StringReader("Alice\nA001\nPassword issue")); 
        cs1.AddNewCustomer(); 
        Console.WriteLine("Serving customer:"); 
        cs1.ServeCustomer();

        // Defect(s) Found: 

        Console.WriteLine("=================");

        // Test 2
        // Scenario: 
        // Expected Result: 
        // Enqueue multiple customers and serve in FIFO order
        Console.WriteLine("Test 2: Add multiple customers, then serve in FIFO order");
        var cs2 = new CustomerService(5);
        Console.SetIn(new System.IO.StringReader(
            "Bob\nB002\nLogin problem\nCharlie\nC003\nPayment issue\nDiana\nD004\nAccount locked"
        ));
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        cs2.AddNewCustomer();
        Console.WriteLine("Serving customer 1:");
        cs2.ServeCustomer(); // Bob
        Console.WriteLine("Serving customer 2:");
        cs2.ServeCustomer(); // Charlie
        Console.WriteLine("Serving customer 3:");
        cs2.ServeCustomer(); // Diana

        // Defect(s) Found: 

        Console.WriteLine("=================");

        // Add more Test Cases As Needed Below

        // Test 3 - Attempt to serve from an empty queue
        Console.WriteLine("Test 3: Serve from empty queue");
        var cs3 = new CustomerService(3);
        cs3.ServeCustomer(); // Should print: No customers in the queue
        Console.WriteLine("=================");

        // Test 4 - Attempt to add more customers than max size
        Console.WriteLine("Test 4: Enqueue more than max size"); 
        var cs4 = new CustomerService(2);
        Console.SetIn(new System.IO.StringReader(
            "Eve\nE005\nPassword\nFrank\nF006\nBilling\nGrace\nG007\nSupport"
        ));
        cs4.AddNewCustomer(); // Eve
        cs4.AddNewCustomer(); // Frank
        cs4.AddNewCustomer(); // Grace -> Should print max queue message
        Console.WriteLine("=================");

        // Test 5 - Display current queue with ToString
        Console.WriteLine("Test 5: Display current queue"); // TEST CODE ADDED
        Console.WriteLine(cs4); // Should show 2 customers in the queue (Eve, Frank)
        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// FIXED: AddNewCustomer now checks >= and is public for testing
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    public void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) { // FIXED: >= instead of >
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// FIXED: ServeCustomer now prints the correct customer and checks empty queue
    /// Dequeue the next customer and display the information.
    /// </summary>
    public void ServeCustomer() {
        if (_queue.Count == 0) { // Prevent error
            Console.WriteLine("No customers in the queue.");
            return;
        }
        var customer = _queue[0]; // Save before removing
        _queue.RemoveAt(0);        // Remove after saving
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}