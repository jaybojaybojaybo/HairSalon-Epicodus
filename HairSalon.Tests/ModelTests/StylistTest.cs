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
  }
}
