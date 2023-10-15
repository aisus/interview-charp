import React, { Component } from "react";
import authService from "./api-authorization/AuthorizeService";

export class BalanceAndOperations extends Component {
  static displayName = BalanceAndOperations.name;

  constructor(props) {
    super(props);
    this.state = {
      balance: {},
      operations: [],
      withdraw: { amount: 0 },
      deposit: { amount: 0 },
      loading: true,
    };
    this.handleDepositAmountChange = this.handleDepositAmountChange.bind(this);
    this.handleWithdrawAmountChange =
      this.handleWithdrawAmountChange.bind(this);
    this.handleWithdrawSubmit = this.handleWithdrawSubmit.bind(this);
    this.handleDepositSubmit = this.handleDepositSubmit.bind(this);
  }

  componentDidMount() {
    this.populateData();
  }

  handleWithdrawAmountChange = (event) => {
    this.setState({
      withdraw: { ...this.state.withdraw, amount: event.target.value },
    });
  };

  handleDepositAmountChange = (event) => {
    this.setState({
      deposit: { ...this.state.deposit, amount: event.target.value },
    });
  };

  handleWithdrawSubmit = async (event) => {
    event.preventDefault();
    await this.withdraw();
  };

  handleDepositSubmit = async (event) => {
    event.preventDefault();
    await this.deposit();
  };

  static renderOperationsTable(operations) {
    return (
      <table className="table table-striped" aria-labelledby="tableLabel">
        <thead>
          <tr>
            <th>Date</th>
            <th>Type</th>
            <th>Balance change</th>
            <th>Message</th>
          </tr>
        </thead>
        <tbody>
          {operations.map((operation) => (
            <tr key={operation.createdDate}>
              <td>{operation.createdDate}</td>
              <td>{operation.type}</td>
              <td>{operation.balanceChange}</td>
              <td>{operation.message}</td>
            </tr>
          ))}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading ? (
      <p>
        <em>Loading...</em>
      </p>
    ) : (
      BalanceAndOperations.renderOperationsTable(this.state.operations)
    );

    let balance = this.state.loading
      ? "..."
      : this.state.balance.currentBalance;

    return (
      <div>
        <h1 id="tableLabel">Balance and operations</h1>
        <p>Current balance: {balance}</p>
        <form onSubmit={this.handleDepositSubmit} class="input-group my-2">
          <input
            type="number"
            class="form-control"
            id="depositInput"
            value={this.state.deposit.amount}
            onChange={this.handleDepositAmountChange}
          />
          <div class="input-group-append">
            <button type="submit" class="btn btn-primary mx-2">
              Deposit
            </button>
          </div>
        </form>
        <form onSubmit={this.handleWithdrawSubmit} class="input-group my-2">
          <input
            type="number"
            class="form-control"
            id="withdrawInput"
            value={this.state.withdraw.amount}
            onChange={this.handleWithdrawAmountChange}
          />
          <div class="input-group-append">
            <button type="submit" class="btn btn-primary mx-2">
              Withdraw
            </button>
          </div>
        </form>
        {contents}
      </div>
    );
  }

  async populateData() {
    const token = await authService.getAccessToken();
    const balanceResponse = await fetch("api/balance", {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    });
    const operationsResponse = await fetch("api/operations", {
      headers: !token ? {} : { Authorization: `Bearer ${token}` },
    });
    const balance = await balanceResponse.json();
    const operations = await operationsResponse.json();
    this.setState({
      balance: balance,
      operations: operations.results,
      loading: false,
    });
  }

  async withdraw() {
    this.setState({ loading: true });
    const token = await authService.getAccessToken();
    const withdrawResponse = await fetch("api/balance/withdraw", {
      method: "POST",
      headers: !token
        ? {}
        : {
            Authorization: `Bearer ${token}`,
            Accept: "application/json",
            "Content-Type": "application/json",
          },
      body: JSON.stringify(this.state.withdraw),
    });

    await this.populateData();
  }

  async deposit() {
    this.setState({ loading: true });
    const token = await authService.getAccessToken();
    const depositResponse = await fetch("api/balance/deposit", {
      method: "POST",
      headers: !token
        ? {}
        : {
            Authorization: `Bearer ${token}`,
            Accept: "application/json",
            "Content-Type": "application/json",
          },
      body: JSON.stringify(this.state.deposit),
    });

    await this.populateData();
  }
}
