import React, { Component } from "react";
import {BrowserRouter, Redirect, Link} from "react-router-dom";
import {logout} from "../../services/logout";

class MedicoConsultas extends Component {
    constructor() {
        super();

        this.state = {
            consultas: [],
            descricao: "",
            idDescricaoIncluir: ""
        }

        this.atualizarDescricao = this.atualizarDescricao.bind(this);
        this.atualizarIdDescricaoIncluir = this.atualizarIdDescricaoIncluir.bind(this);
    }

    

    // lista todas as consultas
    listarConsultas() {
        fetch('http://localhost:5000/api/Consulta', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization" : "Bearer " + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ consultas: data }))
            .catch(erro => console.log(erro))
    }

    // carrega o metodo
    componentDidMount() {
        this.listarConsultas();
    }

    // pega a descriçao digitada
    atualizarDescricao(event) {
        this.setState({ descricao: event.target.value });
    }

    // atualiza a descricao de uma consulta
    atualizarIdDescricaoIncluir(event) {
        this.setState({ idDescricaoIncluir: event.target.value });
    }

    // metodo atualiza descricao prontuario
    incluirDescricao(event) {
        event.preventDefault();
            
        fetch('http://localhost:5000/AlterarDescricaoConsulta', {
            method: "PUT",
            body: JSON.stringify({
                id: this.state.idDescricaoIncluir,
                descricao: this.state.descricao
            }),
            headers: {
                "Content-Type": "application/json",
                
            }
        })
            .then(resposta => resposta)
            .then(this.listarConsultas())
            .catch(erro => console.log(erro))
    }

    render() {
        return (
            <div>
                <table>
                    <tbody>
                        <tr>
                        <th>Id</th>
                        <th>IdProntuario</th>
                        <th>IdMedico</th>
                        <th>Data</th>
                        <th>IdStatus</th>
                        <th>Descricao</th>
                        </tr>

                        {
                            this.state.consultas.map(consulta => {
                                return (
                                    <tr key={consulta.id}>
                                        <td>{consulta.id}</td>
                                        <td>{consulta.idProntuario}</td>
                                        <td>{consulta.idMedico}</td>
                                        <td>{consulta.data.replace("T", " ").split(".")[0]}</td>
                                        <td>{consulta.idStatus}</td>
                                        <td>{consulta.descricao}</td>
                                    </tr>
                                );
                            })
                        }
                    </tbody>
                </table>

                <form onSubmit={this.incluirDescricao.bind(this)}>
                    <input type="text" placeholder="Descricao" value={this.state.descricao} onChange={this.atualizarDescricao} />
                    <input type="text" placeholder="Id" value={this.state.idDescricaoIncluir} onChange={this.atualizarIdDescricaoIncluir} />

                    <button type="submit">Editar</button>
                </form>

                <Link to="/" onClick={logout}>Sair</Link>
            </div>
        );
    }
}

export default MedicoConsultas;