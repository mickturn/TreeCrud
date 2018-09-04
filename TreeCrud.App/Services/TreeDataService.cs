using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TreeCrud.App.Services
{
    public class TreeDataService
    {


        /*
        
        Replace this class by your data layer.

        */
        private static bool _isLoaded = false;
        private static List<TreeNode> nodes = new List<TreeNode> {
                        new TreeNode { Id = 1, ParentId = null, Label = "Unitat 0", Description = "Hooa", Type = "Unitat" },
                        new TreeNode { Id = 2, ParentId = null, Label = "Unitat 1", Description = "Hooa 2", Type = "Unitat" },
                        new TreeNode { Id = 3, ParentId = null, Label = "Unitat 2", Description = "Hooa 2", Type = "Unitat" },
                        new TreeNode { Id = 4, ParentId = null, Label = "Unitat 3", Description = "Hooa 2", Type = "Unitat" },
                        };

        public Task<TreeNode[]> GetNodesAsync(int? ParentId = null)
        {
            loadFakeData();

            TreeNode[] aux_nodes = nodes.Where(x => x.ParentId == ParentId).ToArray();

            return Task.FromResult(aux_nodes);

        }

        public Task<TreeNode[]> GetNodesToAsync(int Id)
        {
            loadFakeData();
            List<TreeNode> aux_nodes = new List<TreeNode>();
            TreeNode auxNode = new TreeNode();
            auxNode.ParentId = Id;
            do {
                auxNode = nodes.Where(x => x.Id == auxNode.ParentId).FirstOrDefault();
                if (auxNode!=null) {
                    aux_nodes.Add(auxNode);
                }
            } while( auxNode != null && auxNode.ParentId!=null );
            aux_nodes.Reverse();
            return Task.FromResult(aux_nodes.ToArray() );
        }

        public Task<TreeNode> GetNodeAsync(int Id)
        {
            loadFakeData();

            TreeNode aux_node = nodes.Where(x => x.Id == Id).FirstOrDefault();

            return Task.FromResult(aux_node);
        }

        public Task<TreeNode> AddUnitatAsync(TreeNode unitat)
        {
            unitat.Id = nodes.Select(X => X.Id).Max() + 1;
            nodes.Add(unitat);
            return Task.FromResult(unitat);
        }

        private static void loadFakeData()
        {
            if (_isLoaded) return;

            int counter = nodes.Select(x => x.Id).Max() + 1;
            _isLoaded = true;
            List<int> nodesWithChildren = new List<int>();
            while (counter < 300)
            {
                var rng1 = new Random();
                int elementAt = rng1.Next(0, nodes.Count());
                int ParentId = nodes.ElementAt(elementAt).Id;
                if (!nodesWithChildren.Contains(ParentId))
                {
                    nodesWithChildren.Add(ParentId);
                    var rng = new Random();
                    int notesToApped = rng.Next(0, 5);
                    IEnumerable<TreeNode> children = Enumerable
                                            .Range(0, notesToApped)
                                            .Select(x =>
                                                   new TreeNode
                                                   {
                                                       Id = counter + x,
                                                       ParentId = ParentId,
                                                       Label = nodes
                                                                           .Where(y => y.Id == ParentId)
                                                                           .Select(y => y.Label)
                                                                           .FirstOrDefault() + x.ToString(),
                                                       Description = "Hooa",
                                                       Type = "Unitat"
                                                   }
                                                    );
                    nodes = nodes.Concat(children).ToList<TreeNode>();
                    counter += notesToApped;
                }
            }

            var rootNodes = nodes.Where(x => !x.ParentId.HasValue);
            foreach (TreeNode node in rootNodes)
            {
                _ = countMyChildren(node);
            }
        }

        private static int countMyChildren(TreeNode node)
        {
            var children = nodes.Where(x => x.ParentId == node.Id);
            int c = children.Count();
            foreach (TreeNode child in children)
            {
                c += countMyChildren(child);
            }
            node.Label = node.Label + " (" + c.ToString("#,##0") + " children)";
            return c;
        }
    }
}
