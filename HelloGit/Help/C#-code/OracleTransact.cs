// Database Setup, if you have not done so yet.
/*
connect scott/tiger@oracle
DROP TABLE MyTable;
CREATE TABLE MyTable (MyColumn NUMBER);
--CREATE TABLE MyTable (MyColumn NUMBER PRIMARY KEY);
 
*/
 
// C#
 
using System;
using System.Data;
using Oracle.DataAccess.Client;
 
class OracleTransactionSample
{
  static void Main()
  {
    // Drop & Create MyTable as indicated Database Setup, at beginning
    
    // This sample starts a transaction and inserts two records with the same
    // value for MyColumn into MyTable.
    // If MyColumn is not a primary key, the transaction will commit.
    // If MyColumn is a primary key, the second insert will violate the 
    // unique constraint and the transaction will rollback.
 
 
    string constr = "User Id=scott;Password=tiger;Data Source=oracle";
    OracleConnection con = new OracleConnection(constr);
    con.Open();
 
    OracleCommand cmd = con.CreateCommand();
 
    // Check the number of rows in MyTable before transaction
    cmd.CommandText = "SELECT COUNT(*) FROM MyTable";    
    int myTableCount = int.Parse(cmd.ExecuteScalar().ToString());
 
    // Print the number of rows in MyTable
    Console.WriteLine("myTableCount = " + myTableCount);
 
    // Start a transaction
    OracleTransaction txn = con.BeginTransaction(
      IsolationLevel.ReadCommitted);
 
    try
    {
      // Insert the same row twice into MyTable
      cmd.CommandText = "INSERT INTO MyTable VALUES (1)";
      cmd.ExecuteNonQuery();
      cmd.ExecuteNonQuery(); // This may throw an exception
      txn.Commit();
    }
    catch (Exception e)
    {
      // Print the exception message
      Console.WriteLine("e.Message    =  " + e.Message);
 
      // Rollback the transaction
      txn.Rollback();
    }    
 
    // Check the number of rows in MyTable after transaction    
    cmd.CommandText = "SELECT COUNT(*) FROM MyTable";
    myTableCount = int.Parse(cmd.ExecuteScalar().ToString());
 
    // Prints the number of rows
    // If MyColumn is not a PRIMARY KEY, the value should increase by two.
    // If MyColumn is a PRIMARY KEY, the value should remain same.
    Console.WriteLine("myTableCount = " + myTableCount);
 
    txn.Dispose();
    cmd.Dispose();
 
    con.Close();
    con.Dispose();
  }
}
