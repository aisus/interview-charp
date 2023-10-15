import React, { Component } from "react";
import authService from "./api-authorization/AuthorizeService";

export class Game extends Component {
  static displayName = Game.name;

  constructor(props) {
    super(props);
    this.state = {
      balance: {},
      gameResult: {},
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
    console.log(this.state);
  };

  handleSubmit = async (event) => {
    event.preventDefault();
    await this.makeABet();
  };

  render() {
    let balance = this.state.loading
      ? "..."
      : this.state.balance.currentBalance;

    return (
      <div>
        <h1 id="tableLabel">Play</h1>
        <p>Current balance: {balance}</p>
        <form onSubmit={this.handleSubmit}>
          <input
            type="number"
            value={this.state.gameInput.stake}
            onChange={this.handleStakeChange}
          />
          <input
            type="number"
            value={this.state.gameInput.number}
            onChange={this.handleNumberChange}
          />
          <button type="submit">Make a bet</button>
        </form>
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
