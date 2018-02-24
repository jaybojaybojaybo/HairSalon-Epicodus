using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class ClientTest : IDisposable
  {
    public void ClientTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=jasun_feddema_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
      Client.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Client.GetAll().Count;

      //
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_ClientList()
    {
      //Arrange
      string clientName = "Client";
      Client newClient = new Client(clientName, 1);

      //Act
      newClient.Save();
      List<Client> result = Client.GetAll();
      List<Client> testList = new List<Client>{newClient};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfStylistNamesAreTheSame_Client()
    {
      //Arrange
      Client firstClient = new Client("Client", 1);
      Client secondClient = new Client("Client", 1);

      //Act
      firstClient.Save();
      secondClient.Save();

      //Assert
      Assert.AreEqual(true, firstClient.GetClientName().Equals(secondClient.GetClientName()));
    }

    [TestMethod]
    public void GetAll_ReturnsClients_ClientList()
    {
      //Arrange
      string name1 = "Client";
      string name2 = "Yu";
      Client client1 = new Client(name1, 1);
      Client client2 = new Client(name2, 1);
      List<Client> newList = new List<Client> {client1, client2};

      //Act
      client1.Save();
      client2.Save();
      List<Client> result = Client.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      string name = "Client";
      Client newClient = new Client(name, 1);
      newClient.Save();

      //Act
      Client savedClient = Client.GetAll()[0];
      int result = savedClient.GetId();
      int testId = newClient.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }

    [TestMethod]
    public void Find_FindsClientInDatabase_Client()
    {
      //Arrange
      Client newClient = new Client("Client", 1);
      newClient.Save();

      //Act
      Client foundClient = Client.Find(newClient.GetId());

      //Assert
      Assert.AreEqual(true, newClient.Equals(foundClient));
    }

    [TestMethod]
    public void FindStylist_RetrieveStylistNameByForeignKey_Stylist()
    {
      //Arrange
      Client newClient = new Client("Client", 1);
      newClient.Save();
      Stylist newStylist = new Stylist("Kim");
      newStylist.Save();

      //Act
      Stylist foundStylist = newClient.FindStylist(newClient.GetStylistId());

      //Assert
      Assert.AreEqual("Kim", foundStylist.GetStylistName());
    }

    [TestMethod]
    public void GimmeTheName_ReturnStylistNameForOneElementOfList_Stylist()
    {
      //Arrange
      Stylist newStylist = new Stylist("Kim");
      newStylist.Save();
      Client newClient = new Client("Client", 1);
      newClient.Save();
      Client newClient2 = new Client("Wu", 1);
      newClient2.Save();
      List<Client> clientList = new List<Client>{newClient, newClient2};
      //Act
      string foundStylist = newClient.GimmeTheName(clientList);
      Console.WriteLine(foundStylist);
      //Assert
      Assert.AreEqual("Kim", foundStylist);
    }
  }
}
