import React, { Component } from 'react';
import authService from './api-authorization/AuthorizeService'

export class BalanceAndOperations extends Component {
  static displayName = BalanceAndOperations.name;

  constructor(props) {
    super(props);
    this.state = { balance: {}, operations: [], loading: true };
  }

  componentDidMount() {
    this.populateData();
  }

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
          {operations.map(operation =>
            <tr key={operation.createdDate}>
              <td>{operation.createdDate}</td>
              <td>{operation.type}</td>
              <td>{operation.balanceChange}</td>
              <td>{operation.message}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>Loading...</em></p>
      : BalanceAndOperations.renderOperationsTable(this.state.operations);

    let balance = this.state.loading
      ? "..."
      : this.state.balance.currentBalance;

    return (
      <div>
        <h1 id="tableLabel">Balance and operations</h1>
        <p>Current balance: {balance}</p>
        {contents}
      </div>
    );
  }

  async populateData() {
    const token = await authService.getAccessToken();
    const balanceResponse = await fetch('api/balance', {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const operationsResponse = await fetch('api/operations', {
      headers: !token ? {} : { 'Authorization': `Bearer ${token}` }
    });
    const balance = await balanceResponse.json();
    const operations = await operationsResponse.json();
    this.setState({ balance: balance, operations: operations.results, loading: false });
  }
}
