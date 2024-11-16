# example-angular-spartan
本リポジトリは Angular + Spartan NG を使用したアプリケーションのサンプルソースコードです。

- Angular : Web アプリケーションのフロントエンドフレームワーク
  - https://angular.dev/
- Spartan NG : Angular で利用可能な UI コンポーネントツール
  - https://www.spartan.ng/

## 環境構築
ご使用のマシンに Node.js が導入済みの前提で記載します。

まず、本リポジトリをマシンに `git clone` でクローンします。

その後、以下のコマンドで node モジュールをインストールします。

```bash
# リポジトリ ディレクトリに移動
cd ./example-angular-spartan

# node モジュールインストール
npm i
```

以上で、環境構築は完了です。

## 起動

### サーバ起動
以下のコマンドで、サーバを起動します。

```bash
npm run start
```

サーバ起動完了後、Web ブラウザで以下の URL にアクセスすると、アプリを利用できます。

```bash
http://localhost:4200
```

### サーバ停止
ターミナルにて、以下のコマンドで、サーバを停止します。

| OS | コマンド |
|---|---|
| macOS / Linux | control + C |
| Windows | Ctrl + C |
