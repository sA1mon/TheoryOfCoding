   M S B A M L   `   `   `  ����  TheoryOfCoding    TheoryOfCoding.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ^ ZICSharpCode.AvalonEdit, Version=6.1.1.20, Culture=neutral, PublicKeyToken=9cc39be672370310?
avalonedit.http://icsharpcode.net/sharpdevelop/avalonedit  5   	     � Title$   Theory of coding (KB-3 SO)��5   	   $	��546��6,   $��923.2��69    � 
ResizeMode$ NoResize=�6G    � Cursor$ Arrow��6]   .��5	      �  � RowDefinitions 5
   
   �� 5      $��7* �6   �� 5      $	��27* �6   �� 5      $	��29* �6   �� 5      $	��23* �6   �� 5      $
��417* �6   �� 5      $��7* �6    � ColumnDefinitions 5   
   �� 5      $��7* �6   �� 5      $
��182* �6   �� 5      $	��12* �6   �� 5      $
��248* �6   �� 5      $	��17* �6   �� 5      $
��138* �6   �� 5      $
��138* �6   �� 5      $	��25* �6   �� 5      $
��142* �6   �� 5      $��7* �6   .+�5   
   �� -   5        $��	CompRatio��5      $��2��6!   $��6��6.   $��3��6>   6T   �� 5   
   -   5        $��CodingTypeComboBox��5   Z   $��Center=�6   $��Center=�67   $��3��6�   $��1��6�   .@�5       ��  �� 
IsSelected . .��5!      �� 	Haffmen6%   5"      �� 5#      .��5$      �� Fano-Shannon6*   5%      �� 5&      .��5'      �� 
Arithmetic6(   5(      �� 5)      .��5*      �� 	BWT + RLE6'   5+      �� 5,      .��5-      �� LZ776"   5.      55      �� 56   
   -   5        $��EncodeButton��56      $��Encode��6%   $��6��6Q   $��1��6a   �� 57   
   -   5        $��DecodeButton��57      $��Decode��6%   $��8��6R   $��1��6b   �� 58   
   -   5        $��
FileButton��58      $��Choose the file...��6#   $��1��6Y   $��1��6i   '  !ICSharpCode.AvalonEdit.TextEditor  59   
   -   5        $��InitialAvalon��59          
IsReadOnly .  "   VerticalScrollBarVisibility$ Visible=�6H   $��1��6o   $��3��6   $��4��6�      WordWrap . 
���   �$��1q�6�   $��15(�6�   	   Text.	 6�   5:        5;   
   -   5        $��TransformedAvalon��5;        .  $ Visible=�6L   $��5��6r   $��4��6�   $��4��6�    . 
���   �$��1q�6�   $��15(�6�   .	 6�   5<      �� 5=   
   $��Initial text��6   $��1��6'   $��3��67   �� 5>   
   $��Transformated text��6   $��5��6-   $��3��6>   �� 5?   
   -   5        $��FilePathLabel��5?      $�� ��6%   $��12(�60   $��1��6>   $��2��6N   $��3��6[   6q   5A      5B      