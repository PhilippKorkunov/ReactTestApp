import {Layout, Col, Row} from 'antd';
import styles from "./Lauout.module.scss";
import MyCard from "../MyCard/MyCard";
import {useEffect, useState} from "react";
import axios from "axios";

const {
    Header, Footer,
    Sider, Content
} = Layout;

const headerStyle = {
    textAlign: 'center',
    height: "50px",
    color: '#fff',
    paddingInline: 50,
    lineHeight: '50px',
    backgroundColor: '#bed3f3',
};
const contentStyle = {
    minHeight: 280,
    overflow : 'hidden',
    padding: 15,
    textAlign: 'center',
    color: '#fff',
    backgroundColor: 'white',
};
const leftSiderStyle = {
    minHeight: 280,
    textAlign: 'center',
    lineHeight: '250px',
    color: '#fff',
    backgroundColor: 'grey',
};
const rightSiderStyle = {

    textAlign: 'center',
    lineHeight: '200px',
    color: '#fff',
    backgroundColor: 'grey',
};
const footerStyle = {
    display: "flex",
    alignItems: "center",
    justifyContent: "center",
    height: "100px",
    minHeight: "100px",
    textAlign: 'center',
    lineHeight: '100px',
    color: '#fff',
    backgroundColor: '#bed3f3',
};

const App = () => {

    const [data, setData] = useState([])

    useEffect(async () => {
        const currentData = await axios.get("https://localhost:7116/api/TestApp/GetOnePagePosts", { params: { pageNumber : 1, pageSize : 48 } })
         setData(currentData.data)
    }, [])

    console.log(data);

   

       
    return (
        <Layout className={styles.wrapper}>
            <Header style={headerStyle}>Header</Header>
            <Layout>
                <Sider style={leftSiderStyle}>Sider</Sider>
                <Content style={contentStyle}>
                    <Row>
                        {data.map((item, index) => {
                            return (
                                <Col>
                                    <MyCard key={index} content={item.title} index={index + 1}/>
                                </Col>
                            )
                        })}
                    </Row>
                </Content>
                <Sider style={rightSiderStyle}>Sider</Sider>
            </Layout>
            <Footer style={footerStyle}>Footer</Footer>
        </Layout>
    );
}
export default App;