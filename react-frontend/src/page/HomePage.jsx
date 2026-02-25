import { Button, Card, Col, Row, Space, Statistic, Tag, Typography } from 'antd'
import {
  CheckCircleOutlined,
  ClockCircleOutlined,
  EnvironmentOutlined,
  PhoneOutlined,
  SafetyCertificateOutlined,
} from '@ant-design/icons'
import { Link } from 'react-router-dom'
import { ROUTES } from '../Constants/routes'
import { usePageTitle } from '../hooks/usePageTitle'
import { restaurantInfo } from '../lib/restaurantInfo'

function HomePage() {
  usePageTitle('Home')

  return (
    <Space direction="vertical" size={24} className="page-stack">
      <Card className="hero-card">
        <Row gutter={[24, 24]} align="middle">
          <Col xs={24} lg={14}>
            <Tag color="red" className="hero-tag">
              Welcome to Hola Restaurant
            </Tag>
            <Typography.Title className="hero-title">{restaurantInfo.name}</Typography.Title>
            <Typography.Paragraph className="hero-description">
              {restaurantInfo.tagline}
            </Typography.Paragraph>
            <Typography.Paragraph className="hero-description secondary">
              {restaurantInfo.description}
            </Typography.Paragraph>
            <Space wrap>
              <Button type="primary" size="large">
                <Link to={ROUTES.LOGIN}>Staff Login</Link>
              </Button>
              <Button size="large">
                <Link to={ROUTES.FORGET_PASSWORD}>Reset Password</Link>
              </Button>
            </Space>
          </Col>

          <Col xs={24} lg={10}>
            <Card className="info-panel" bordered={false}>
              <Space direction="vertical" size={12}>
                <Typography.Title level={4} className="m0">
                  Contact & Opening Hours
                </Typography.Title>
                <Space>
                  <EnvironmentOutlined />
                  <Typography.Text>{restaurantInfo.address}</Typography.Text>
                </Space>
                <Space>
                  <PhoneOutlined />
                  <Typography.Text>{restaurantInfo.phone}</Typography.Text>
                </Space>
                <Space>
                  <CheckCircleOutlined />
                  <Typography.Text>{restaurantInfo.email}</Typography.Text>
                </Space>
                <Space>
                  <ClockCircleOutlined />
                  <Typography.Text>{restaurantInfo.openingHours}</Typography.Text>
                </Space>
              </Space>
            </Card>
          </Col>
        </Row>
      </Card>

      <Row gutter={[16, 16]}>
        <Col xs={24} md={8}>
          <Card>
            <Statistic title="Opening Hours" value="10:00 - 22:00" />
          </Card>
        </Col>
        <Col xs={24} md={8}>
          <Card>
            <Statistic title="Dining Style" value="Family & Business" />
          </Card>
        </Col>
        <Col xs={24} md={8}>
          <Card>
            <Statistic title="Location" value="Hoa Lac" />
          </Card>
        </Col>
      </Row>

      <Card title="About Hola Restaurant">
        <Row gutter={[16, 16]}>
          <Col xs={24} md={8}>
            <Card className="feature-card" bordered={false}>
              <CheckCircleOutlined className="feature-icon" />
              <Typography.Title level={5}>Quality Ingredients</Typography.Title>
              <Typography.Paragraph>
                Every dish is prepared from carefully selected ingredients to ensure freshness,
                consistency, and reliable taste quality.
              </Typography.Paragraph>
            </Card>
          </Col>
          <Col xs={24} md={8}>
            <Card className="feature-card" bordered={false}>
              <SafetyCertificateOutlined className="feature-icon" />
              <Typography.Title level={5}>Professional Service</Typography.Title>
              <Typography.Paragraph>
                Our team delivers attentive service for daily dining, partner meetings, and group
                events in a comfortable and organized environment.
              </Typography.Paragraph>
            </Card>
          </Col>
          <Col xs={24} md={8}>
            <Card className="feature-card" bordered={false}>
              <ClockCircleOutlined className="feature-icon" />
              <Typography.Title level={5}>Flexible Dining Time</Typography.Title>
              <Typography.Paragraph>
                With all-day service, guests can plan lunch, dinner, and gatherings with ease at
                Hola Restaurant in Hoa Lac.
              </Typography.Paragraph>
            </Card>
          </Col>
        </Row>
      </Card>

      <Card title="Menu Highlights">
        <Row gutter={[16, 16]}>
          <Col xs={24} md={12} lg={6}>
            <Card bordered={false} className="feature-card">
              <Typography.Title level={5}>Starters</Typography.Title>
              <Typography.Paragraph className="m0">
                Light dishes designed to begin the meal comfortably and prepare the palate.
              </Typography.Paragraph>
            </Card>
          </Col>
          <Col xs={24} md={12} lg={6}>
            <Card bordered={false} className="feature-card">
              <Typography.Title level={5}>Main Courses</Typography.Title>
              <Typography.Paragraph className="m0">
                Signature dishes suitable for shared tables, family dining, and business meals.
              </Typography.Paragraph>
            </Card>
          </Col>
          <Col xs={24} md={12} lg={6}>
            <Card bordered={false} className="feature-card">
              <Typography.Title level={5}>Desserts</Typography.Title>
              <Typography.Paragraph className="m0">
                Balanced finishing options to complete the dining experience.
              </Typography.Paragraph>
            </Card>
          </Col>
          <Col xs={24} md={12} lg={6}>
            <Card bordered={false} className="feature-card">
              <Typography.Title level={5}>Beverages</Typography.Title>
              <Typography.Paragraph className="m0">
                A selection of drinks that pairs well with meals and group gatherings.
              </Typography.Paragraph>
            </Card>
          </Col>
        </Row>
      </Card>
    </Space>
  )
}

export default HomePage
