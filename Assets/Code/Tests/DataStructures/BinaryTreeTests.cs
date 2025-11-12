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
        public void BinaryTree_PreOrder_1()
        {
            var tree = GetTree_1();
            var preOrder = tree.PreOrder();

            Assert.AreEqual(7, preOrder.Count);

            Assert.AreEqual(17, preOrder[0]);
            Assert.AreEqual(41, preOrder[1]);
            Assert.AreEqual(29, preOrder[2]);
            Assert.AreEqual(6, preOrder[3]);
            Assert.AreEqual(9, preOrder[4]);
            Assert.AreEqual(81, preOrder[5]);
            Assert.AreEqual(40, preOrder[6]);
        }
        [Test]
        public void BinaryTree_1()
        {
   
        }
    }
}