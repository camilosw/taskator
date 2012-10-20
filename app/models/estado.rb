class Estado < ActiveRecord::Base
  attr_accessible :nombre
  has_many :tareas

  validates :nombre, presence: true
end

# == Schema Information
#
# Table name: estados
#
#  id         :integer          not null, primary key
#  nombre     :string(255)      not null
#  created_at :datetime         not null
#  updated_at :datetime         not null
#

