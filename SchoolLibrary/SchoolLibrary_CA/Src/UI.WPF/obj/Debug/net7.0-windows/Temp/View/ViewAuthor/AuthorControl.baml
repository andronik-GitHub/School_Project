   M S B A M L   `   `   `  ����A  =UI.WPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:UI.WPF.Temp.View.ViewAuthorUI.WPF.Temp.View.ViewAuthor  5      /    )UI.WPF.Temp.View.ViewAuthor.AuthorControl   5      W SPresentationCore, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=7.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=7.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ;local)clr-namespace:UI.WPF.Temp.View.ViewAuthor     5      .��5
      � ���#00CC66  � RowDefinitions  5   
   �� 5      $��80��6   �� 5      $��80��6   �� 5      $��* �6   .+�5   
   �� $��Authors��6   $��25(�6#   $
��Bold"�61   $��Center=�6C   $��Center=�6_   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"    � 	Resources 5       �� 
TargetType �� Property �� Value��%�   '��       �� 5      " ��6   .�5      ��  � ��$	 100��6.   �� 5       � ��$ 40��6/   �� 5       � ��$
 15 0q�6/   5      5      .+�5      �� -   5         ��Name$ Count��5      $��Count: ��6'   $��15 15 50 15q�66   $��20(�6K   $
��Bold"�6Y   $��Center=�6k   �� 5      -   5        $	��Add��5      6#   �� 5      -   5        $
��Edit��5      6$   �� 5      -   5        $��Delete��5      6&   5       �� 5"   
   -   5         ��Name$ 	itemsList��5"      $��2��6&   $��15q�63    �� View 5$      �� 5&      .P�5(      �� $��AuthorId��6T   	 �� Width$		 250��6f   
 �� DisplayMemberBinding
 6%   ��  �� Path$ AuthorId�6f   �� 5)      $��	FirstName��6U   $		 250��6h   
 6%   �� $ 	FirstName�6h   �� 5*      $��LastName��6T   $		 250��6f   
 6%   �� $ LastName�6f   �� 5+      $��	Birthdate��6U   $		 150��6h   
 6%   �� $ 	Birthdate�6h   �� 5,      $��Nationality��6W   $		 150��6l   
 6%   �� $ Nationality�6l   5.      51      52      53      