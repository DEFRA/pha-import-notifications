using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using NSubstitute;
using PhaImportNotifications.Utils.Mongo;

namespace PhaImportNotifications.Tests.Utils.Mongo
{
    public class MongoServiceTests
    {
        private readonly IMongoDbClientFactory _connectionFactoryMock;
        private readonly IMongoCollection<TestModel> _collectionMock;

        private readonly TestMongoService _service;

        public MongoServiceTests()
        {
            _connectionFactoryMock = Substitute.For<IMongoDbClientFactory>();
            _collectionMock = Substitute.For<IMongoCollection<TestModel>>();

            _connectionFactoryMock.GetClient().Returns(Substitute.For<IMongoClient>());

            _connectionFactoryMock.GetCollection<TestModel>(Arg.Any<string>()).Returns(_collectionMock);

            _collectionMock.CollectionNamespace.Returns(new CollectionNamespace("test", "example"));
            _collectionMock.Database.DatabaseNamespace.Returns(new DatabaseNamespace("test"));

            _service = new TestMongoService(_connectionFactoryMock, "testCollection", NullLoggerFactory.Instance);

            _collectionMock.DidNotReceive().Indexes.CreateMany(Arg.Any<IEnumerable<CreateIndexModel<TestModel>>>());
        }

        [Fact]
        public void EnsureIndexes_CreatesIndexes_WhenIndexesAreDefined()
        {
            var indexes = new List<CreateIndexModel<TestModel>>()
            {
                new CreateIndexModel<TestModel>(Builders<TestModel>.IndexKeys.Ascending(x => x.Name)),
            };
            _service.SetIndexes(indexes);
            _service.RunEnsureIndexes();

            _collectionMock.Received(1).Indexes.CreateMany(indexes);
        }

        [Fact]
        public void EnsureIndexes_DoesNotCreateIndexes_WhenIndexesAreNotDefined()
        {
            _service.SetIndexes(new List<CreateIndexModel<TestModel>>());
            _service.RunEnsureIndexes();

            _collectionMock.DidNotReceive().Indexes.CreateMany(Arg.Any<IEnumerable<CreateIndexModel<TestModel>>>());
        }

        public class TestModel
        {
            public string? Name { get; set; }
        }

        public interface ITestMongoService
        {
            public List<CreateIndexModel<TestModel>> GetIndexes();
            public void SetIndexes(List<CreateIndexModel<TestModel>> indexes);
        }

        private class TestMongoService : MongoService<TestModel>, ITestMongoService
        {
            protected List<CreateIndexModel<TestModel>> Indexes = new List<CreateIndexModel<TestModel>>();

            public TestMongoService(
                IMongoDbClientFactory connectionFactory,
                string collectionName,
                ILoggerFactory loggerFactory
            )
                : base(connectionFactory, collectionName, loggerFactory) { }

            public List<CreateIndexModel<TestModel>> GetIndexes()
            {
                return Indexes;
            }

            public void SetIndexes(List<CreateIndexModel<TestModel>> indexes)
            {
                this.Indexes = indexes;
            }

            protected override List<CreateIndexModel<TestModel>> DefineIndexes(
                IndexKeysDefinitionBuilder<TestModel> builder
            )
            {
                if (GetIndexes() == null)
                {
                    throw new System.Exception("Indexes not defined");
                }
                return GetIndexes();
            }

            public void RunEnsureIndexes()
            {
                base.EnsureIndexes();
            }
        }
    }
}
