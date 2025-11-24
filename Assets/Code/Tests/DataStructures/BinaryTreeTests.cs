using NUnit.Framework;

namespace CodePractice.Tests
{
    public class BinaryTreeTests
    {
        private BinaryTree<MyInt> GetTree_1()
        {
            var tree = new BinaryTree<MyInt>();
            tree.Root = new BinaryTreeNode<MyInt>
            {
                Item = 17,
                Left = new BinaryTreeNode<MyInt>
                {
                    Item = 41,
                    Left = new BinaryTreeNode<MyInt>
                    {
                        Item = 29,
                    },
                    Right = new BinaryTreeNode<MyInt>
                    {
                        Item = 6
                    }
                },
                Right = new BinaryTreeNode<MyInt>()
                {
                    Item = 9,
                    Left = new BinaryTreeNode<MyInt>()
                    {
                        Item = 81
                    },
                    Right = new BinaryTreeNode<MyInt>()
                    {
                        Item = 40,
                    }
                }
            };

            return tree;
        }

        [Test]
        public void BinaryTree_GetMax()
        {
            Assert.AreEqual(81, GetTree_1().GetMax());
        }

        [Test]
        public void BinaryTree_PreOrder_1()
        {
            var tree = GetTree_1();
            var preOrder = tree.PreOrder();

            Assert.AreEqual(7, preOrder.Count);

            Assert.AreEqual(17, preOrder[0].Value);
            Assert.AreEqual(41, preOrder[1].Value);
            Assert.AreEqual(29, preOrder[2].Value);
            Assert.AreEqual(6, preOrder[3].Value);
            Assert.AreEqual(9, preOrder[4].Value);
            Assert.AreEqual(81, preOrder[5].Value);
            Assert.AreEqual(40, preOrder[6].Value);
        }

        [Test]
        public void BinaryTree_InOrder_1()
        {
            var tree = GetTree_1();
            var inOrder = tree.InOrder();

            Assert.AreEqual(7, inOrder.Count);

            Assert.AreEqual(29, inOrder[0].Value);
            Assert.AreEqual(41, inOrder[1].Value);
            Assert.AreEqual(6, inOrder[2].Value);
            Assert.AreEqual(17, inOrder[3].Value);
            Assert.AreEqual(81, inOrder[4].Value);
            Assert.AreEqual(9, inOrder[5].Value);
            Assert.AreEqual(40, inOrder[6].Value);
        }

        [Test]
        public void BinaryTree_PostOrder_1()
        {
            var tree = GetTree_1();
            var postOrder = tree.PostOrder();

            Assert.AreEqual(7, postOrder.Count);

            Assert.AreEqual(29, postOrder[0].Value);
            Assert.AreEqual(6, postOrder[1].Value);
            Assert.AreEqual(41, postOrder[2].Value);
            Assert.AreEqual(81, postOrder[3].Value);
            Assert.AreEqual(40, postOrder[4].Value);
            Assert.AreEqual(9, postOrder[5].Value);
            Assert.AreEqual(17, postOrder[6].Value);
        }
    }
}