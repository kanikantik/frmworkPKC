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
    public class CategoryServiceTests
    {
        private readonly DbSet<Category> mockDatabaseSet;
        private readonly DataContext mockDataContext;
        private readonly IUnitOfWorkAsync mockUnitOfWork;

        public CategoryServiceTests()
        {
            mockDataContext = Substitute.For<DataContext>();
            mockDatabaseSet = Substitute.For<DbSet<Category>>();
            mockUnitOfWork = Substitute.For<IUnitOfWorkAsync>();
        }

        [TestMethod]
        public void Category_Find_DbMock()
        {
            object[] name = new object[3];
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.Find(Arg.Any<object[]>()).Returns(new Category());
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            var result = categoryService.Find(name);
        }

        [TestMethod]
        public void Category_SelectQuery()
        {
            string query = "select * from categories";
            object[] name = new object[3];
            var sqlQueryMock = Substitute.For<DbSqlQuery<Category>>();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.SqlQuery(Arg.Any<string>(), Arg.Any<object[]>()).Returns(sqlQueryMock);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            var result = categoryService.SelectQuery(query, name);
        }

        [TestMethod]
        public void Category_Insert()
        {
            var category = new Category();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.Add(Arg.Any<Category>()).Returns(category);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.Insert(new Category());
        }

        [TestMethod]
        public void Category_InsertRange()
        {
            var list = new List<Category>();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.AddRange(Arg.Any<List<Category>>()).Returns(list);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.InsertRange(list);
        }

        [TestMethod]
        public void Category_Update()
        {
            var category = new Category();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.Attach(Arg.Any<Category>()).Returns(category);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.Update(category);
        }

        [TestMethod]
        public void Category_UpdateRange()
        {
            var list = new List<Category>();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.Attach(Arg.Any<Category>()).Returns(new Category());
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.UpdateRange(list);
        }

        [TestMethod]
        public void Category_InsertGraphRange()
        {
            var list = new List<Category>();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.AddRange(Arg.Any<List<Category>>()).Returns(list);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.InsertGraphRange(list);
        }

        [TestMethod]
        public void Category_Delete()
        {
            var category = new Category();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.Remove(Arg.Any<Category>()).Returns(category);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.Delete(category);
        }

        [TestMethod]
        public void Category_RemoveRange()
        {
            var list = new List<Category>();
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.RemoveRange(Arg.Any<List<Category>>()).Returns(list);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.DeleteRange(list);
        }

        [TestMethod]
        public void Category_Query()
        {
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            categoryService.Query();
        }

        [TestMethod]
        public void Category_Query_Expression()
        {
            var category = new Category();
            Expression<Func<Category, bool>> query = cat => cat.Equals(category);
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            var result = categoryService.Query(query);
        }

        [TestMethod]
        public void Category_Queryable()
        {
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            var result = categoryService.Queryable();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Category_FindAsync()
        {
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
        }

        [TestMethod]
        public void Category_FindAsync_CancellationToken()
        {
            object[] keyValues = { "category" };
            Task<Category> task = new Task<Category>(() => new Category());
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.FindAsync(Arg.Any<CancellationToken>(), Arg.Any<object[]>()).Returns(task);
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            var result = categoryService.FindAsync(CancellationToken.None, keyValues);
            //Assert.IsNotNull(result.CategoryId);
            //Assert.IsNotNull(result.CategoryName);
            //Assert.IsNotNull(result.Description);
        }

        [TestMethod]
        public void Category_DeleteAsync()
        {
            object[] keyValues = { "category" };
            Task<Category> task = new Task<Category>(() => new Category());
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.FindAsync(Arg.Any<CancellationToken>(), Arg.Any<object[]>()).Returns(task);
            mockDatabaseSet.Attach(Arg.Any<Category>()).Returns(new Category());
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            var result = categoryService.DeleteAsync(keyValues);
        }

        [TestMethod]
        public void Category_DeleteAsync_Cancellation_Token()
        {
            object[] keyValues = { "category" };
            Task<Category> task = new Task<Category>(() => new Category());
            mockDataContext.Set<Category>().Returns(mockDatabaseSet);
            mockDatabaseSet.FindAsync(Arg.Any<CancellationToken>(), Arg.Any<object[]>()).Returns(task);
            mockDatabaseSet.Attach(Arg.Any<Category>()).Returns(new Category());
            var categoryRepository = new CategoryRepository(mockDataContext, mockUnitOfWork);
            var categoryService = new CategoryService(categoryRepository);
            var result = categoryService.DeleteAsync(CancellationToken.None, keyValues);
        }
    }
}