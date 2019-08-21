﻿/*
Anystatus Elasticsearch plugin
Copyright (C) 2019  Enterprisecoding (Fatih Boy)

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU Affero General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU Affero General Public License for more details.

You should have received a copy of the GNU Affero General Public License
along with this program.  If not, see <http://www.gnu.org/licenses/>.
 */
using AnyStatus.API;
using AnyStatus.API.Common.Utils;
using AnyStatus.Plugins.Elasticsearch.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AnyStatus.Plugins.Elasticsearch.Index.IndexStoreSize
{
    public class IndexStoreSizeMetricQuery : IMetricQuery<IndexStoreSizeWidget>
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
                    request.DataContext.Value = BytesFormatter.Format(Convert.ToInt64(clusterStatsResponse.Indices[documentCountWidget.IndexName].Primaries.Store.SizeInBytes));
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
                    request.DataContext.Value = BytesFormatter.Format(Convert.ToInt64(clusterStatsResponse.Indices[documentCountWidget.IndexName].Total.Store.SizeInBytes));
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