using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using NSubstitute;
using frmworkPKC.Entities;
using frmworkPKC.Repository;
using frmworkPKC.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using frmworkPKC.Repository.Ef6;
using frmworkPKC.Repository.Pattern.UnitOfWork;

namespace frmworkPKC.Services.UnitTest
{
    [TestClass]
    public class SupplierServiceTests
    {
        private readonly DbSet<Supplier> mockDatabaseSet;
        private readonly DataContext mockDataContext;
        private readonly IUnitOfWorkAsync mockUnitOfWork;

        public SupplierServiceTests()
        {
            mockDataContext = Substitute.For<DataContext>();
            mockDatabaseSet = Substitute.For<DbSet<Supplier>>();
            mockUnitOfWork = Substitute.For<IUnitOfWorkAsync>();
        }

        [TestMethod]
        public void Supplier_Find_DbMock()
        {
            object[] name = new object[3];
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.Find(Arg.Any<object[]>()).Returns(new Supplier());
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            var result = SupplierService.Find(name);
        }

        [TestMethod]
        public void Supplier_SelectQuery()
        {
            string query = "select * from categories";
            object[] name = new object[3];
            var sqlQueryMock = Substitute.For<DbSqlQuery<Supplier>>();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.SqlQuery(Arg.Any<string>(), Arg.Any<object[]>()).Returns(sqlQueryMock);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            var result = SupplierService.SelectQuery(query, name);
        }

        [TestMethod]
        public void Supplier_Insert()
        {
            var Supplier = new Supplier();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.Add(Arg.Any<Supplier>()).Returns(Supplier);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.Insert(new Supplier());
        }

        [TestMethod]
        public void Supplier_InsertRange()
        {
            var list = new List<Supplier>();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.AddRange(Arg.Any<List<Supplier>>()).Returns(list);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.InsertRange(list);
        }

        [TestMethod]
        public void Supplier_Update()
        {
            var Supplier = new Supplier();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.Attach(Arg.Any<Supplier>()).Returns(Supplier);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.Update(Supplier);
        }

        [TestMethod]
        public void Supplier_UpdateRange()
        {
            var list = new List<Supplier>();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.Attach(Arg.Any<Supplier>()).Returns(new Supplier());
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.UpdateRange(list);
        }

        [TestMethod]
        public void Supplier_InsertGraphRange()
        {
            var list = new List<Supplier>();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.AddRange(Arg.Any<List<Supplier>>()).Returns(list);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.InsertGraphRange(list);
        }

        [TestMethod]
        public void Supplier_Delete()
        {
            var Supplier = new Supplier();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.Remove(Arg.Any<Supplier>()).Returns(Supplier);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.Delete(Supplier);
        }

        [TestMethod]
        public void Supplier_RemoveRange()
        {
            var list = new List<Supplier>();
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.RemoveRange(Arg.Any<List<Supplier>>()).Returns(list);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.DeleteRange(list);
        }

        [TestMethod]
        public void Supplier_Query()
        {
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            SupplierService.Query();
        }

        [TestMethod]
        public void Supplier_Query_Expression()
        {
            var Supplier = new Supplier();
            Expression<Func<Supplier, bool>> query = cat => cat.Equals(Supplier);
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            var result = SupplierService.Query(query);
        }

        [TestMethod]
        public void Supplier_Queryable()
        {
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            var result = SupplierService.Queryable();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Supplier_FindAsync()
        {
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
        }

        [TestMethod]
        public void Supplier_FindAsync_CancellationToken()
        {
            object[] keyValues = { "Supplier" };
            Task<Supplier> task = new Task<Supplier>(() => new Supplier());
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.FindAsync(Arg.Any<CancellationToken>(), Arg.Any<object[]>()).Returns(task);
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            var result = SupplierService.FindAsync(CancellationToken.None, keyValues);
            //Assert.IsNotNull(result.SupplierId);
            //Assert.IsNotNull(result.SupplierName);
            //Assert.IsNotNull(result.Description);
        }

        [TestMethod]
        public void Supplier_DeleteAsync()
        {
            object[] keyValues = { "Supplier" };
            Task<Supplier> task = new Task<Supplier>(() => new Supplier());
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.FindAsync(Arg.Any<CancellationToken>(), Arg.Any<object[]>()).Returns(task);
            mockDatabaseSet.Attach(Arg.Any<Supplier>()).Returns(new Supplier());
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            var result = SupplierService.DeleteAsync(keyValues);
        }

        [TestMethod]
        public void Supplier_DeleteAsync_Cancellation_Token()
        {
            object[] keyValues = { "Supplier" };
            Task<Supplier> task = new Task<Supplier>(() => new Supplier());
            mockDataContext.Set<Supplier>().Returns(mockDatabaseSet);
            mockDatabaseSet.FindAsync(Arg.Any<CancellationToken>(), Arg.Any<object[]>()).Returns(task);
            mockDatabaseSet.Attach(Arg.Any<Supplier>()).Returns(new Supplier());
            var SupplierRepository = new SupplierRepository(mockDataContext, mockUnitOfWork);
            var SupplierService = new SupplierService(SupplierRepository);
            var result = SupplierService.DeleteAsync(CancellationToken.None, keyValues);
        }
    }
}
