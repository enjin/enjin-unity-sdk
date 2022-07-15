/* Copyright 2021 Enjin Pte. Ltd.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace Enjin.SDK.Graphql
{
    /// <summary>
    /// Class for registering and storing GraphQL templates.
    /// </summary>
    [PublicAPI]
    public class GraphqlQueryRegistry
    {
        private static readonly string[] SCHEMAS = {"player", "project", "shared",};
        private static readonly string[] TEMPLATE_TYPES = {"fragment", "mutation", "query",};

        private readonly Dictionary<string, GraphqlTemplate> _fragments = new Dictionary<string, GraphqlTemplate>();
        private readonly Dictionary<string, GraphqlTemplate> _operations = new Dictionary<string, GraphqlTemplate>();

        /// <summary>
        /// Sole constructor.
        /// </summary>
        public GraphqlQueryRegistry()
        {
            RegisterSdkTemplates();
        }

        private void LoadAndCacheTemplateContents([CanBeNull] string[] contents, TemplateType templateType)
        {
            if (contents == null)
                return;

            var id = GraphqlTemplate.ReadNamespace(contents);
            if (id == null)
                return;

            if (templateType == TemplateType.FRAGMENT)
                _fragments.Add(id, new GraphqlTemplate(id, templateType, contents, _fragments));
            else if (templateType == TemplateType.MUTATION || templateType == TemplateType.QUERY)
                _operations.Add(id, new GraphqlTemplate(id, templateType, contents, _fragments));
        }

        private void LoadTemplatesInResources()
        {
            foreach (var schema in SCHEMAS)
            {
                foreach (var templateType in TEMPLATE_TYPES)
                {
                    if (!Enum.TryParse(templateType, true, out TemplateType type))
                        continue;

                    var path = $"Templates/enjin/schemas/{schema}/{templateType}";
                    var resources = Resources.LoadAll(path, typeof(TextAsset)).Cast<TextAsset>().ToArray();
                    foreach (var resource in resources)
                    {
                        LoadAndCacheTemplateContents(resource.text.Split('\n'), type);
                        Resources.UnloadAsset(resource);
                    }
                }
            }

            foreach (var operation in _operations.Values)
            {
                operation.Compile();
            }
        }

        public void RegisterTemplatesInResources()
        {
            LoadTemplatesInResources();
        }

        internal void RegisterSdkTemplates()
        {
            RegisterTemplatesInResources();
        }

        /// <summary>
        /// Gets the template that is registered under the name provided.
        /// </summary>
        /// <param name="name">The name of the template.</param>
        /// <returns>The template if one exists, else <c>null</c>.</returns>
        [CanBeNull]
        public GraphqlTemplate GetOperationForName(string name)
        {
            return _operations.ContainsKey(name)
                ? _operations[name]
                : null;
        }
    }
}