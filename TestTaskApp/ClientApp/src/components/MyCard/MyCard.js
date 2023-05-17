import {Card} from 'antd';
import React from 'react';
import styles from "./MyCard.module.scss"

const MyCard = ({content, index}) => {
    return (
        <div className={styles.wrapper}>
            <Card className={styles.card} title={"Карточка № " + index}>
                {content || "content"}
            </Card>
        </div>
    );
};
export default MyCard;