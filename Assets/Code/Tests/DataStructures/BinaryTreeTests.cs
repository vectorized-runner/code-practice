using NUnit.Framework;

namespace CodePractice.Tests
{
    public class BinaryTreeTests
    {
        private BinaryTree<int> GetTree_1()
        {
            var tree = new BinaryTree<int>();
            tree.Root = new BinaryTreeNode<int>
            {
                Item = 17,
                Left = new BinaryTreeNode<int>
                {
                    Item = 41,
                    Left = new BinaryTreeNode<int>
                    {
                        Item = 29,
                    },
                    Right = new BinaryTreeNode<int>
                    {
                        Item = 6
                    }
                },
                Right = new BinaryTreeNode<int>()
                {
                    Item = 9,
                    Left = new BinaryTreeNode<int>()
                    {
                        Item = 81
                    },
                    Right = new BinaryTreeNode<int>()
                    {
                        Item = 40,
                    }
                }
            };

            return tree;
        }
        
        [Test]
        public void BinaryTree_1()
        {
   
        }
    }
}