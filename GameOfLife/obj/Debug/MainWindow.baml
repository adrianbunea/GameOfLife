   M S B A M L   `   `   `  ����E  AGameOfLife, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:GameOfLife
GameOfLife  5   	       GameOfLife.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   X TXceed.Wpf.Toolkit, Version=3.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5   	   $localclr-namespace:GameOfLife  5   	   UwpfxJhttp://schemas.microsoft.com/winfx/2006/xaml/presentation/toolkit/extended  5   	     � Title$  
MainWindow��5
   	   $	��750��6   $	��600��6)   ���	#FF363636.��5      �  � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $	��600 �6   �� 5      $��4* �6   .+�5   
   o� -   5        $
��Menu��5      $��0��6   .@�5      m� $��Pattern��6   .@�5      m� -   5        $��	GliderGun��5      6.   m� 5      -   5        $��Pulsar��5      6+   m� 5      -   5        $
��LWSS��5      6)   m� 5      -   5        $��	Butterfly��5      6.   m� 5      -   5        $��	Shipmaker��5      6.   m� 5      -   5        $��Random��5      6+   5      5      �� 5   
   $��1��66   ��6   �� *Cells+��5      �� 5       .R�5!      A�  A� Columns 6"   �� *GridColumns+5"      ��5%      �� 5&      .R�5'      *  $System.Windows.Controls.DataGridCell  -   5        $	��0.1q�5'   #   ��5(      �� 5)       �� Color 6.   �� *Color+5+      5,      5.      � 50   
   $��2��6    � ColumnDefinitions 51      �� 52      $
��150* �6#   �� 53      $
��400* �6#   �� 54      $
��100* �6#   .+�57      �� $��0��6   .+�58      �� $
��Rows��6   $��14(�6'   
�������$
��Left=�6H   $��	10 5 10 5q�6j   �� 59      $��Columns��6   $��14(�6*   
�������$
��Left=�6K   $��	10 5 10 5q�6m   5:      �� 5<      $��0��6   .+�5=      %  Xceed.Wpf.Toolkit.IntegerUpDown  -	   5        $��
RowsUpDown��5=   %   $��10 10 10 10q�67   $��45��6L   $��20��6W   $��12(�6c   $��Right=�6�   �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   Value 6q   �� *
GridRows+6�     5>      -
   5        $��ColumnsUpDown��5>   %   $��10 10 10 10q�6:   $��45��6O   $��20��6Z   $��12(�6f   $��Right=�6�    6t   �� *GridColumns+6�   5?      �� 5A      $��1��6   $��Generation Rate��6$   $��20(�6>   
�������$��Center=�6_   $	��Top=�6�   $��10 20 10 25q�6�   �� 5B      $��1��6    � Minimum$ 1Q�6@    � Maximum$ 20Q�6L   $��Center=�6Y   $��Bottom=�6}   $��10 30 10 15q�6�    � Value 6%   �� *
Interval+6�   �� 5D      -   5        $��Start��5D      $��2��6%   $��10 10 10 10q�65   $��10 10 10 10q�6J   6`   5E      5F      5G      