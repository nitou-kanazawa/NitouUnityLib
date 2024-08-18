# Nitou Unity Lib

## 【概要】
Unityでの開発で頻繁に使用するコードを試験的にパッケージとして切り出している．
パッケージ群は[Basic]と[Aditional]の２層に分類され，
全プロジェクトで共通して使用するモジュールは[Basic]，
オプションとして追加したいモジュールは[Aditional]に振り分ける．

![image](https://github.com/user-attachments/assets/4e5f3e04-d02f-4463-b678-91145a08c979)

※PackageManagerで自作パッケージの依存関係を解決（自動インストール）する方法が分かっていないため，
できるだけパッケージ数は少なくしたい

## 【構成】
導入するコードを選択可能にするため，以下のように複数パッケージに分ける．
（※あまりアセンブリファイルを増やしたくないので，分割方法は使いながら調整していく）

- Lib
- Basic Modules
- Basic Tools
- Framwork

--- 

#### Lib 
基本的な拡張メソッドや Math 関連の汎用クラス（構造体）を備えたライブラリ．

#### Main Modules

#### Tools

#### Framwork
