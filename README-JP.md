## Unique Asset Bundle Tool
独自のクラスや構造体をアセットバンドルにできるツールです。
![window](https://github.com/HiroshiMinakata/UniqueBundler/blob/main/Image/window.png)

## ファイルの保存方法
- バイナリ
- GZIP圧縮バイナリ
- AES暗号化バイナリ
- GZIP圧縮+AES暗号化バイナリ

## 使い方
1. `ClassConfig.yaml`と`Extensions.yaml`を[指定のフォーマット](https://github.com/HiroshiMinakata/UniqueBundler/wiki/コンフィグフォーマット)で設定してください。
2. `AddFile`、`Add Folder`、 もしくはドラッグアンドドロップでファイルを追加
3. ファイルデータの必要がない場合は手動でクラスの選択と、各項目を入力してください。
4. `Save` 保存方法を選択してください。[詳細](https://github.com/HiroshiMinakata/UniqueBundler/wiki/保存方法)
5. 読み込む際は[ファイルフォーマット](https://github.com/HiroshiMinakata/UniqueBundler/wiki/ファイルフォーマット)に従ってバイナリとして読み込んでください。
6. `Load` アセットバンドルの内容を変更するには保存方法と同じ項目を選択してください。
