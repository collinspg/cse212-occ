using Microsoft.VisualStudio.TestTools.UnitTesting;

// TODO Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Enqueue a single item and then dequeue it.
    // Expected Result: The same item is returned.
    // Defect(s) Found: N/A 
    public void TestPriorityQueue_SingleItem()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 5);
        Assert.AreEqual("A", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with different priorities
    // Expected Result: Dequeue returns the item with the highest priority
    // Defect(s) Found: N/A
    public void TestPriorityQueue_HighestPriority()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 1);
        pq.Enqueue("B", 3);
        pq.Enqueue("C", 2);

        Assert.AreEqual("B", pq.Dequeue()); // highest priority
        Assert.AreEqual("C", pq.Dequeue());
        Assert.AreEqual("A", pq.Dequeue());
    }

    // Add more test cases as needed below.

    [TestMethod]
    // Scenario: Enqueue multiple items with same highest priority
    // Expected Result: Dequeue returns the first item with the highest priority (FIFO)
    // Defect(s) Found: N/A
    public void TestPriorityQueue_FIFOTie()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 3);
        pq.Enqueue("B", 3);
        pq.Enqueue("C", 2);

        Assert.AreEqual("A", pq.Dequeue()); // FIFO tie
        Assert.AreEqual("B", pq.Dequeue());
        Assert.AreEqual("C", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Enqueue items, then dequeue all and verify order of remaining items
    // Expected Result: Items are removed in correct priority order
    // Defect(s) Found: N/A
    public void TestPriorityQueue_OrderPreservation()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 2);
        pq.Enqueue("B", 1);
        pq.Enqueue("C", 2);
        pq.Enqueue("D", 3);

        Assert.AreEqual("D", pq.Dequeue());
        Assert.AreEqual("A", pq.Dequeue());
        Assert.AreEqual("C", pq.Dequeue());
        Assert.AreEqual("B", pq.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue
    // Expected Result: InvalidOperationException is thrown with correct message
    // Defect(s) Found: N/A
    public void TestPriorityQueue_EmptyQueueException()
    {
        var pq = new PriorityQueue();
        try
        {
            pq.Dequeue();
            Assert.Fail("Expected exception not thrown.");
        }
        catch (InvalidOperationException e)
        {
            Assert.AreEqual("The queue is empty.", e.Message);
        }
    }

    [TestMethod]
    // Scenario: Enqueue multiple items with negative and zero priorities
    // Expected Result: Highest priority (largest number) is returned correctly
    // Defect(s) Found: N/A
    public void TestPriorityQueue_NegativeAndZeroPriorities()
    {
        var pq = new PriorityQueue();
        pq.Enqueue("A", 0);
        pq.Enqueue("B", -2);
        pq.Enqueue("C", 1);

        Assert.AreEqual("C", pq.Dequeue());
        Assert.AreEqual("A", pq.Dequeue());
        Assert.AreEqual("B", pq.Dequeue());
    }

}