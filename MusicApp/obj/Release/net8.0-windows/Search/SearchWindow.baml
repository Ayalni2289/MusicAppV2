   M S B A M L   `   `   `  ����   MusicApp"    MusicApp.Search.SearchWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Search - Music App��$	��600��$	��800�����#121212$2��,Helvetica Neue, Helvetica, Arial, sans-serif)�.���� $
��Auto=�.���  � ColumnDefinitions �� $��* � � RowDefinitions �� $
��Auto ��� $��* �.+��� ���#191414���#888$��1q� �� CornerRadius$ 10��$��40q�._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��Search��$��24(����#9f61e2$��Center=�$
��0,20q��� -    ��Name$ 
searchArea��$��1��$��20q�.+��� -    ��Name$ searchInput��$��Enter keywords to search�����#333
�������$��10q�$
��0,10q�$��0q�
������ �� -    ��Name$ filterComboBox�� �� SelectedIndex$ 0��$
��0,10q�.@��� $��	No filter���� $
��Song���� $��Artist���� $��Album���� $��Genre���� $
��User���� -   $ 
genreInput��$��Enter genre to filter�����#333
�������$��10q�$
��0,10q�$��0q�
������ $y�	Collapsed=��� -   $ sortComboBox��$ 0��$
��0,10q�.@��� $��	Relevance���� $��
Popularity���� $��Release Date���� $��Alphabetical���� -   $��Search�����#1DB954
�������$��10q�$
��0,10q�$��0q�$��Stretch=��� -   $ searchResultsStackPanel��$��1��$
��Auto��$
��Auto��$��
427,60,0,0q�$��2��$	��Top=�$
��Left=�