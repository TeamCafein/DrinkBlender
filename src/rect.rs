use liquidfun::box2d::{
    collision::shapes::polygon_shape::PolygonShape,
    common::math::Vec2,
    dynamics::{
        body::{Body, BodyDef},
        fixture::FixtureDef,
        world::World,
    },
};
use crate::drawable::Drawable;

pub struct Rect {
    body: Body,
}

impl Rect {
    pub fn new(world: &mut World, x: f32, y: f32, width: f32, height: f32) -> Self {
        let mut body_def = BodyDef::default();
        body_def.position.set(x, y);

        let body = world.create_body(&body_def);

        let mut shape = PolygonShape::new();
        shape.set_as_box(width, height);

        body.create_fixture(&FixtureDef::new(&shape));
        Self { body }
    }

    pub fn get_position(&self) -> &Vec2 {
        self.body.get_position()
    }
}

impl Drawable for Rect {

}
