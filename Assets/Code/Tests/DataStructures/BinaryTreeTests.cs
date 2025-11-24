using System.Runtime.Remoting;
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

        private BinaryTree<MyInt> GetBST()
        {
            return new BinaryTree<MyInt>()
            {
                Root = new BinaryTreeNode<MyInt>
                {
                    Item = 55,
                    Left = new BinaryTreeNode<MyInt>
                    {
                        Item = 29,
                        Left = new BinaryTreeNode<MyInt>
                        {
                            Item = -3
                        },
                        Right = new BinaryTreeNode<MyInt>
                        {
                            Item = 42
                        }
                    },
                    Right = new BinaryTreeNode<MyInt>
                    {
                        Item = 87,
                        Left = new BinaryTreeNode<MyInt>
                        {
                            Item = 60
                        },
                        Right = new BinaryTreeNode<MyInt>
                        {
                            Item = 91
                        }
                    }
                }
            };
        }

        [Test]
        public void IsEmpty_1()
        {
            Assert.IsFalse(GetBST().IsEmpty());
        }

        [Test]
        public void IsEmpty_2()
        {
            Assert.IsFalse(GetTree_1().IsEmpty());
        }

        [Test]
        public void IsEmpty_3()
        {
            Assert.IsTrue(new BinaryTree<MyInt>().IsEmpty());
        }

        [Test]
        public void BST_Contains_1()
        {
            var bst = GetBST();
            Assert.IsTrue(bst.BST_Contains(29));
            Assert.IsTrue(bst.BST_Contains(55));
            Assert.IsTrue(bst.BST_Contains(-3));
            Assert.IsTrue(bst.BST_Contains(42));
            Assert.IsTrue(bst.BST_Contains(87));
            Assert.IsTrue(bst.BST_Contains(60));
            Assert.IsTrue(bst.BST_Contains(91));
            
            Assert.IsFalse(bst.BST_Contains(-1));
            Assert.IsFalse(bst.BST_Contains(0));
            Assert.IsFalse(bst.BST_Contains(3498));
        }

        [Test]
        public void BinaryTree_IsFull()
        {
            Assert.IsTrue(GetTree_1().IsFull());
        }

        [Test]
        public void BinaryTree_CountLeaves()
        {
            Assert.AreEqual(4, GetTree_1().CountLeaves());
        }

        [Test]
        public void BinaryTree_GetHeight()
        {
            Assert.AreEqual(2, GetTree_1().GetHeight());
        }

        [Test]
        public void BinaryTree_Sum()
        {
            Assert.AreEqual(
                17 + 41 + 29 + 6 + 9 + 81 + 40, GetTree_1().Sum());
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