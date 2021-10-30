# C# SFML + IMGUI 
<img src="https://github.com/witcherofthorns/csharp-sfml-imgui/blob/master/csharp_imgui_sfml.gif" width=90% />
</br>
This is a project for game creation, OpenGL window context creation in SFML and input control, implemented in C# </br>
All ImGui draw calls are called from the classic Nuget ImGui.Net package. </br>

### Warning!
This solution is not 100% stable and does not give any guarantees since this is a port. </br> This solution was written solely for educational purposes </br>

## How to used?
See the Source in <a href="https://github.com/witcherofthorns/csharp-sfml-imgui/blob/master/Program.cs"><b>Program.cs</b></a>

```Csharp
while (renderWindow.IsOpen) {
  // ...
  ImGui.ShowDemoWindow();   // imgui.net native
  imgui.Render();           // imgui object
  // ...
}
```

Easy imgui menu bar
```Csharp
  ImGui.Begin("menu", ImGuiWindowFlags.MenuBar);
  if(ImGui.BeginMenuBar())
  {
      if(ImGui.BeginMenu("File"))
      {
          if(ImGui.MenuItem("Open", "Ctrl+O")) { /* Do stuff */ }
          if (ImGui.MenuItem("Save", "Ctrl+S")) { /* Do stuff */ }
          if (ImGui.MenuItem("Close", "Ctrl+W")) { /* Do stuff */ }
          ImGui.EndMenu();
      }
      ImGui.EndMenuBar();
  }
  ImGui.End();
```
