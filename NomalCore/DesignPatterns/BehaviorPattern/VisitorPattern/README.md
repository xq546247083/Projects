# 访问者模式 Visitor

## Intro

> 访问者模式（Visitor），表示一个作用于某对象结构中的各元素的操作，它使你可以在不改变各元素的类的前提下定义作用于这些元素的新操作。

## 使用场景

访问者模式的目的时要把处理从数据结构分离出来，有比较稳定的数据结构，又有易于变化的算法时，使用访问者模式就是比较适合的，因为访问者模式使得算法操作的增加变得容易。反之，如果数据结构对象易于变化，经常有新的数据对象增加进来，就不适合使用访问者模式。

## 优缺点

优点：

- 增加新的操作很容易，增加新的操作就意味着增加一个新的访问者，访问者模式将有关的行为集中到一个访问者对象中。

缺点：

- 增加新的数据结构困难，破坏 开放封闭 原则

## More

更多设计模式及示例代码 [传送门](https://github.com/WeihanLi/DesignPatterns)