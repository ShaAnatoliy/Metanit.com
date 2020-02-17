### C# Assembly Info...
```c#
System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
MessageBox.Show(assembly.CodeBase, "CodeBase"); // или assembly.EscapedCodeBase – то-же
```
![Рис.1](https://github.com/ShaAnatoliy/DevOps/raw/master/img/asm1.png)

```c#
MessageBox.Show(assembly.Location, "assembly.Location");
```
![Рис.2](https://github.com/ShaAnatoliy/DevOps/raw/master/img/asm2.png)

```c#
MessageBox.Show(assembly.FullName, "assembly.FullName");
```
![Рис.3](https://github.com/ShaAnatoliy/DevOps/raw/master/img/asm3.png)
```c#
MessageBox.Show(assembly.ImageRuntimeVersion, "assembly.ImageRuntimeVersion");
```
![Рис.4](https://github.com/ShaAnatoliy/DevOps/raw/master/img/asm4.png)
