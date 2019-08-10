﻿using AnyStatus.API;
using AnyStatus.Plugins.Elasticsearch.Cluster.FileSystemUsage;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace AnyStatus.Plugins.Elasticsearch.Tests.Widgets.Cluster
{
    [TestClass]
    public class FileSystemUsageTests
    {
        [TestMethod]
        public async Task FileSystemUsage()
        {
            var fileSystemUsageTestsWidget = new FileSystemUsageWidget
            {
                NodeUris = new List<string>() { "http://127.0.0.1:9200" },
                PercentageType = Shared.FileSystemPercentageType.PercentageUsed
            };
            var request = MetricQueryRequest.Create(fileSystemUsageTestsWidget);
            var metricQuery = new FileSystemUsageHandler();

            await metricQuery.Handle(request, CancellationToken.None);

            Assert.AreSame(State.Ok, request.DataContext.State);
        }

        [TestMethod]
        public async Task FileSystemUsageSecure()
        {
            var fileSystemUsageTestsWidget = new FileSystemUsageWidget
            {
                NodeUris = new List<string>() { "https://127.0.0.1:9200" },
                TrustCertificate = true,
                UseBasicAuthentication = true,
                Username = "elastic",
                Password = "changeme",
                PercentageType = Shared.FileSystemPercentageType.PercentageUsed
            };
            var request = MetricQueryRequest.Create(fileSystemUsageTestsWidget);
            var metricQuery = new FileSystemUsageHandler();

            await metricQuery.Handle(request, CancellationToken.None);

            Assert.AreSame(State.Ok, request.DataContext.State);
        }
    }
}