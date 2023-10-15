import React, { Component } from "react";
import authService from "./api-authorization/AuthorizeService";

export class Game extends Component {
  static displayName = Game.name;

  constructor(props) {
    super(props);
    this.state = {
      balance: {},
      gameResult: null,
      gameInput: { stake: 0, number: 5 },
      loading: true,
    };
    this.handleStakeChange = this.handleStakeChange.bind(this);
    this.handleNumberChange = this.handleNumberChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  componentDidMount() {
    this.populateData();
  }

  handleStakeChange = (event) => {
    this.setState({
      gameInput: { ...this.state.gameInput, stake: event.target.value },
    });
  };

  handleNumberChange = (event) => {
    this.setState({
      gameInput: { ...this.state.gameInput, number: event.target.value },
    });
  };

  handleSubmit = async (event) => {
    event.preventDefault();
    await this.makeABet();
  };

  render() {
    let balance = this.state.loading
      ? "..."
      : this.state.balance.currentBalance;

    let lastGameResult =
      this.state.gameResult === null ? (
        <p></p>
      ) : (
        <div>
          <p>{this.state.gameResult.gameWon ? "You won!" : "You lost!"}</p>
          <p>
            Winning number: {this.state.gameResult.winningNumber}, balance
            change: {this.state.gameResult.balanceChange}
          </p>
        </div>
      );
    return (
      <div>
        <h1 id="tableLabel">Play</h1>
        <p>Current balance: {balance}</p>
        <form onSubmit={this.handleSubmit}>
          <div class="form-group">
            <label for="stakeInput">Stake:</label>
            <input
              type="number"
              class="form-control"
              id="stakeInput"
              value={this.state.gameInput.stake}
              onChange={this.handleStakeChange}
            />
          </div>
          <div class="form-group">
            <label for="numberInput">Number:</label>
            <input
              type="number"
              class="form-control"
              id="numberInput"
              value={this.state.gameInput.number}
              onChange={this.handleNumberChange}
            />
          </div>
          <button type="submit" class="btn btn-primary my-2">
            Make a bet
          </button>
        </form>
        <br />
        {lastGameResult}
      </div>
    );
  }

  async populateData() {
    const token = await authService.getAccessToken();
    const balanceResponse = await fetch("api/balance", {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    });
    const balance = await balanceResponse.json();
    this.setState({ balance: balance, loading: false });
  }

  async makeABet() {
    this.setState({ loading: true });
    const token = await authService.getAccessToken();
    const gameResponse = await fetch("api/game", {
      method: "POST",
      headers: !token
        ? {}
        : {
            Authorization: `Bearer ${token}`,
            Accept: "application/json",
            "Content-Type": "application/json",
          },
      body: JSON.stringify(this.state.gameInput),
    });
    const gameResult = await gameResponse.json();
    this.setState({ gameResult: gameResult });

    await this.populateData();
  }
}
