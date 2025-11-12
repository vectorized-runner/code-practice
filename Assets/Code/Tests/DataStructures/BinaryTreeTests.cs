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
        public void BinaryTree_InOrder_1()
        {
            var tree = GetTree_1();
            var inOrder = tree.InOrder();

            Assert.AreEqual(7, inOrder.Count);

            Assert.AreEqual(29, inOrder[0]);
            Assert.AreEqual(41, inOrder[1]);
            Assert.AreEqual(6, inOrder[2]);
            Assert.AreEqual(17, inOrder[3]);
            Assert.AreEqual(81, inOrder[4]);
            Assert.AreEqual(9, inOrder[5]);
            Assert.AreEqual(40, inOrder[6]);
        }

        [Test]
        public void BinaryTree_PostOrder_1()
        {
            var tree = GetTree_1();
            var postOrder = tree.PostOrder();

            Assert.AreEqual(7, postOrder.Count);

            Assert.AreEqual(29, postOrder[0]);
            Assert.AreEqual(6, postOrder[1]);
            Assert.AreEqual(41, postOrder[2]);
            Assert.AreEqual(81, postOrder[3]);
            Assert.AreEqual(40, postOrder[4]);
            Assert.AreEqual(9, postOrder[5]);
            Assert.AreEqual(17, postOrder[6]);
        }
    }
}