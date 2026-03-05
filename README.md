# クラス構成

```mermaid
classDiagram
namespace TicTacToe_Interfaces {
class IPlayer {
<<interface>>
+string Name
+CellValueType InputType
+CellState DecidePlacement(Board board)
}
}

    namespace TicTacToe_Implements {
        class User {
            +string Name
            +CellValueType InputType
            +User(string name, CellValueType cellValueType)
            +CellState DecidePlacement(Board board)
        }

        class WeakCpu {
            +string Name
            +CellValueType InputType
            +CellState DecidePlacement(Board board)
        }

        class StrongCpu {
            +string Name
            +CellValueType InputType
            +CellState DecidePlacement(Board board)
        }
    }

    IPlayer <|.. User
    IPlayer <|.. WeakCpu
    IPlayer <|.. StrongCpu
```

<br/>

- IPlayer<br/>
  ゲームプレイヤー・CPU 両者が持つ共通の振る舞いをインターフェースとして定義<br/>

- User<br/>
  ゲームプレイヤーの振る舞いを実装<br/>

- WeakPlayer<br/>
  空いているマスを埋めるだけの弱めの CPU の振る舞いを実装<br/>

- StrongPlayer<br/>
  真ん中、四隅、リーチのマスを優先的に埋める強めの CPU の振る舞いを実装<br/>

# シーケンス

**PvC の場合**

```mermaid
sequenceDiagram
    actor User as ConsoleUser
    participant GM as GameMaster
    participant Board as Board
    participant Human as HumanPlayer
    participant CPU as CpuPlayer

    %% 1. モード選択（PvC が選ばれている前提）
    User->>GM: アプリ起動 & SelectMode()
    GM->>User: 「ゲームモード選択のメッセージを表示」
    User->>GM: PvC を選択

    %% 2. ゲーム開始
    User->>GM: Start()

    %% 2-1. 難易度選択
    GM->>User: 「難易度選択メッセージを表示」
    User->>GM: 初級 or 上級 を入力
    GM-->>CPU: CPUレベルを決定（Weak / Strong）

    %% 2-2. 先攻・後攻選択
    GM->>User: 「先攻・後攻選択メッセージを表示」
    User->>GM: 先攻 or 後攻 を入力
    GM-->>Human: あなたの手番（先攻/後攻）を決定
    GM-->>CPU: CPU の手番（先攻/後攻）を決定

    %% 2-3. ゲーム開始合図
    GM->>User: 「ゲーム開始メッセージを表示」
    User->>GM: Enter キー入力
    GM->>Board: 初期盤面を表示
    Board-->>User: 盤面表示

    %% 3. ターン進行（勝敗 or 引き分けまで）
    loop ゲーム終了まで繰り返し
        %% 3-1. 先攻側の手番
        alt 先攻が Human の場合
            GM->>Human: 「手を入力して下さい」と促す
            Human->>User: 入力を要求
            User->>Human: どこに置くか入力
            Human-->>GM: 選んだマス情報を返す
        else 先攻が CPU の場合
            GM->>CPU: 「手を決めて」と依頼
            CPU-->>GM: 選んだマス情報を返す
        end
        GM->>Board: 盤面更新 & 表示
        Board-->>User: 更新後の盤面
        GM-->>User: 勝ち or 引き分けならメッセージ表示（あればループ終了）

        %% 3-2. 後攻側の手番（上と対称）
        alt 後攻が Human の場合
            GM->>Human: 「手を入力して下さい」と促す
            Human->>User: 入力を要求
            User->>Human: どこに置くか入力
            Human-->>GM: 選んだマス情報を返す
        else 後攻が CPU の場合
            GM->>CPU: 「手を決めて」と依頼
            CPU-->>GM: 選んだマス情報を返す
        end
        GM->>Board: 盤面更新 & 表示
        Board-->>User: 更新後の盤面
        GM-->>User: 勝ち or 引き分けならメッセージ表示（あればループ終了）
    end

    %% 4. ゲーム終了
    GM->>User: 「ゲーム終了メッセージを表示」
```

<br/>

**PvP の場合**

```mermaid
sequenceDiagram
    actor User1 as Player1(Console)
    actor User2 as Player2(Console)
    participant GM as GameMaster
    participant Board as Board
    participant P1 as User("1P")
    participant P2 as User("2P")

    %% 1. モード選択（PvP が選ばれている前提）
    User1->>GM: アプリ起動 & SelectMode()
    GM->>User1: 「ゲームモード選択のメッセージを表示」
    User1->>GM: PvP を選択

    %% 2. ゲーム開始
    User1->>GM: Start()

    %% 2-1. PvP では難易度・先攻後攻選択は無し
    GM-->>P1: 1P（○）プレイヤー生成
    GM-->>P2: 2P（×）プレイヤー生成
    GM-->>GM: P1 を先攻, P2 を後攻に設定

    %% 2-2. ゲーム開始合図
    GM->>User1: 「ゲーム開始メッセージを表示」
    User1->>GM: Enter キー入力
    GM->>Board: 初期盤面を表示
    Board-->>User1: 盤面表示
    Board-->>User2: 同じ盤面を共有して見る想定

    %% 3. ターン進行（勝敗 or 引き分けまで）
    loop ゲーム終了まで繰り返し
        %% 3-1. 先攻（1P）の手番
        GM->>P1: 「手を入力して下さい」と促す
        P1->>User1: 入力を要求
        User1->>P1: どこに置くか入力
        P1-->>GM: 選んだマス情報を返す
        GM->>Board: 盤面更新 & 表示
        Board-->>User1: 更新後の盤面
        Board-->>User2: 更新後の盤面
        GM-->>User1: 勝ち or 引き分けならメッセージ表示（あればループ終了）
        GM-->>User2: 同じメッセージを確認する想定

        %% 3-2. 後攻（2P）の手番
        GM->>P2: 「手を入力して下さい」と促す
        P2->>User2: 入力を要求
        User2->>P2: どこに置くか入力
        P2-->>GM: 選んだマス情報を返す
        GM->>Board: 盤面更新 & 表示
        Board-->>User1: 更新後の盤面
        Board-->>User2: 更新後の盤面
        GM-->>User1: 勝ち or 引き分けならメッセージ表示（あればループ終了）
        GM-->>User2: 同じメッセージを確認する想定
    end

    %% 4. ゲーム終了
    GM->>User1: 「ゲーム終了メッセージを表示」
    GM->>User2: 「ゲーム終了メッセージを共有」
```
