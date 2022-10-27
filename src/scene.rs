use liquidfun::box2d::common::math::Vec2;
use liquidfun::box2d::dynamics::world::World;
use crate::rect::Rect;

pub struct Scene {
    world: World,
    objects: Vec<Rect>
}

impl Scene {
    pub fn new() -> Self {
        let mut world = World::new(&Vec2::new(0.0, -10.0));
        let mut objects = vec![];

        Scene {
            world,
            objects
        }
    }

    fn add(&mut self, object: Rect) {
        self.objects.push(object)
    }
}
