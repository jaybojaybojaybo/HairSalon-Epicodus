using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using HairSalon.Models;

namespace HairSalon.Tests
{
  [TestClass]
  public class StylistTest : IDisposable
  {
    public void StylistTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=jasun_feddema_test;";
    }

    public void Dispose()
    {
      Stylist.DeleteAll();
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtFirst_0()
    {
      //Arrange, Act
      int result = Stylist.GetAll().Count;

      //
      Assert.AreEqual(0, result);
    }

    [TestMethod]
    public void Save_SavesStylistToDatabase_ItemList()
    {
      //Arrange
      string stylistName = "Kim";
      Stylist newStylist = new Stylist(stylistName);

      //Act
      newStylist.Save();
      List<Stylist> result = Stylist.GetAll();
      List<Stylist> testList = new List<Stylist>{newStylist};

      //Assert
      CollectionAssert.AreEqual(result, testList);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfStylistNamesAreTheSame_Stylist()
    {
      //Arrange
      Stylist firstStylist = new Stylist("Kim");
      Stylist secondStylist = new Stylist("Kim");

      //Act
      firstStylist.Save();
      secondStylist.Save();

      //Assert
      Assert.AreEqual(true, firstStylist.GetStylistName().Equals(secondStylist.GetStylistName()));
    }

    [TestMethod]
    public void GetAll_ReturnsStylists_StylistList()
    {
      //Arrange
      string name1 = "Kim";
      string name2 = "John";
      Stylist stylist1 = new Stylist(name1);
      Stylist stylist2 = new Stylist(name2);
      List<Stylist> newList = new List<Stylist> {stylist1, stylist2};

      //Act
      stylist1.Save();
      stylist2.Save();
      List<Stylist> result = Stylist.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToObject_Id()
    {
      //Arrange
      string name = "Kim";
      Stylist newStylist = new Stylist(name);
      newStylist.Save();

      //Act
      Stylist savedStylist = Stylist.GetAll()[0];
      int result = savedStylist.GetId();
      int testId = newStylist.GetId();

      //Assert
      Assert.AreEqual(testId, result);

    }

    [TestMethod]
    public void Find_FindsStylistInDatabase_Stylist()
    {
      //Arrange
      Stylist newStylist = new Stylist("Kim");
      newStylist.Save();

      //Act
      Stylist foundStylist = Stylist.Find(newStylist.GetId());
      
      //Assert
      Assert.AreEqual(true, newStylist.Equals(foundStylist));
    }
  }
}
