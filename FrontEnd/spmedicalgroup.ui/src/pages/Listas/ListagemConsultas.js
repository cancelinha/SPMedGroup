import React, { Component } from "react";
import { Link } from "react-router-dom";
import { logout } from "../../services/logout";
import Table from "../../../node_modules/react-bootstrap/Table"
import "../../../node_modules/bootstrap/dist/css/bootstrap.css"

class ListagemConsultas extends Component {
    constructor() {
        super();
        this.state = {
            consultas: [],
           
        }     
        
    }
    logout() {       
        localStorage.removeItem("userautent-token-spmedicalgroup");        
    }
    // lista todas as consultas
    listarTodasConsultas() {
        fetch('http://192.168.3.215:5000/api/Consulta', {
            method: "GET",
            headers: {
                "Content-Type": "application/json",
                "Authorization": 'Bearer ' + localStorage.getItem("userautent-token-spmedicalgroup")
            }
        })
            .then(resposta => resposta.json())
            .then(data => this.setState({ consultas: data }))
            .catch(erro => console.log(erro))
    }
    // carrega o metodo
    componentDidMount() {
        this.listarTodasConsultas();
    }
    render() {
        return (
            <div className="all">
                        <h1>Lista Atualizada das Consultas:</h1>
                 <Table striped bordered hover size="sm">
                    <tbody> 
                        <tr>
                            <th>ID</th>
                            <th>Nome do Prontuário</th>
                            <th>Nome do Medico</th>
                            <th>Data da Consulta</th>
                            <th>Status</th>
                            <th>Descrição</th>
                        </tr>

                        {
                            this.state.consultas.map(consulta => {
                                return (
                                    <tr key={consulta.id}>
                                        <td>{consulta.id}</td>
                                        <td>{consulta.idProntuarioNavigation.nome}</td>
                                        <td>{consulta.idMedicoNavigation.nome}</td>
                                        <td>{consulta.data.replace("T", " ").split(".")[0]}</td>
                                        <td>{consulta.idStatusNavigation.situacao}</td>
                                        <td>{consulta.descricao}</td>
                                    </tr>
                                );
                            } )
                        }
                    </tbody>
                </Table>
                <Link to="/">
                        <button onClick={this.logout}>Sair</button>
                </Link>
            </div>
        );
    }
}


export default ListagemConsultas;