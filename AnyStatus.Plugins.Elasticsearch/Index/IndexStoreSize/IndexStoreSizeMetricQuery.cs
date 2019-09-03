﻿/*
Anystatus Elasticsearch plugin
Copyright 2019 Fatih Boy

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
 */
using AnyStatus.API;
using AnyStatus.API.Common.Utils;
using AnyStatus.Plugins.Elasticsearch.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AnyStatus.Plugins.Elasticsearch.Index.IndexStoreSize
{
    public class IndexStoreSizeMetricQuery : IRequestHandler<MetricQueryRequest<IndexStoreSizeWidget>>
    {
        /// <summary>
        /// Elasticsearch Helper to retrieve elastic client
        /// </summary>
        private readonly ElasticsearchHelper elasticsearchHelper;

        public IndexStoreSizeMetricQuery() : this(new ElasticsearchHelper()) { }

        /// <summary>
        /// Constructer used by unit tests
        /// </summary>
        /// <param name="elasticsearchHelper">Elasticsearch Helper class instance to use</param>
        internal IndexStoreSizeMetricQuery(ElasticsearchHelper elasticsearchHelper)
        {
            this.elasticsearchHelper = elasticsearchHelper;
        }

        public async Task Handle(MetricQueryRequest<IndexStoreSizeWidget> request, CancellationToken cancellationToken)
        {
            var documentCountWidget = request.DataContext;

            var client = elasticsearchHelper.GetElasticClient(documentCountWidget);

            if (documentCountWidget.SizeType == StoreSizeType.Primary)
            {
                var clusterStatsResponse = await client.IndexStatsAsync(documentCountWidget.IndexName, "indices.*.primaries.store", cancellationToken);

                if (clusterStatsResponse.IsValid)
                {
                    request.DataContext.Value = clusterStatsResponse.Indices[documentCountWidget.IndexName].Primaries.Store.SizeInBytes;
                    request.DataContext.State = State.Ok;
                }
                else
                {
                    documentCountWidget.State = State.Invalid;
                }
            }
            else {
                var clusterStatsResponse = await client.IndexStatsAsync(documentCountWidget.IndexName, "indices.*.total.store", cancellationToken);

                if (clusterStatsResponse.IsValid)
                {
                    request.DataContext.Value = clusterStatsResponse.Indices[documentCountWidget.IndexName].Total.Store.SizeInBytes;
                    request.DataContext.State = State.Ok;
                }
                else
                {
                    documentCountWidget.State = State.Invalid;
                }
            }
        }
    }
}
